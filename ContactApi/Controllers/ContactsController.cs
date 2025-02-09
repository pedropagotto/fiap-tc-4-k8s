using API.Middlewares.Exceptions;
using API.Validation;
using Application.Services;
using Application.ViewModels.Contact;
using Common.QueueMessageModels;
using Domain.Shared;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IBus _bus;

        public ContactsController(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Cadastra um novo contato no sistema.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedErrorModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateContact([FromBody] ContactRequestModel contact)
        {
            ContactValidator validator = new();
            validator.IsValid(contact);

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:create-contact"));

            await endpoint.Send(new CreateContactMessage(contact.Name, contact.Ddd, contact.Phone, contact.Email, $"Criação contato solicitada às {DateTime.Now}"));
            
            return Ok("Solicitação de criação enviada com sucesso!");
        }

        /// <summary>
        /// Atualiza um cadastro no sistema.
        /// </summary>
        /// <param name="id" example="1">Id do contato a ser atualizado.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedErrorModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PutContact([FromRoute] int id, [FromBody] ContactRequestModel contact)
        {
            ContactValidator validator = new();
            validator.IsValid(contact);
            
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:update-contact"));

            await endpoint.Send(new UpdateContactMessage(id, contact.Name, contact.Ddd, contact.Phone, contact.Email, $"Atualização de contato solicitada às {DateTime.Now}"));
            
            return Ok("Solicitação de atualização enviada com sucesso!");
        }

        /// <summary>
        /// Remove um contato do sistema.
        /// </summary>
        /// <param name="id" example="1">Id do contato a ser removido.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedErrorModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteContact([FromRoute] int id)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:remove-contact"));

            await endpoint.Send(new RemoveContactMessage(id, $"Remoção de contato solicitada às {DateTime.Now}"));
            
            return Ok("Solicitação de exclusão enviada com sucesso!");
        }

    }
}