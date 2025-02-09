using API.Middlewares.Exceptions;
using Application.Services;
using Application.ViewModels.Contact;
using Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Lista os contatos cadastrados no sistema. Permite a opção de filtro de contatos pela localização através do DDD.
        /// </summary>
        /// <param name="DDD" example="11">Opção de filtro por região pelo DDD.</param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedErrorModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<ContactResponseModel>>> ListAllContacts([FromQuery] string? DDD = null)
        {
            if (DDD is not null) return Ok(await _contactService.FilterByDdd(DDD));

            return Ok(await _contactService.ListAll());
        }

        /// <summary>
        /// Busca um contato pelo id informado.
        /// </summary>
        /// <param name="id" example="1">Id do contato buscado.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedErrorModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactResponseModel>> GetContact([FromRoute] int id)
        {
            var contact = await _contactService.GetById(id)!;

            if (contact is null)
            {
                NotFoundException.Throw("001", "Contato não encontrado.");
            }

            return Ok(contact!);
        }
    }
}