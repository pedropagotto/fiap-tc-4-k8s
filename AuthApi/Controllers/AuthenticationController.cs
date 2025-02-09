using System.Diagnostics.CodeAnalysis;
using API.Middlewares.Exceptions;
using API.Services.Token;
using Application.Models.Authentication;
using Application.Services.AuthenticationServices;
using Application.ViewModels.Authentication;
using Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IGenerateJwtToken _generateJwtToken;
        private readonly IAuthService _authService;

        public AuthenticationController(IGenerateJwtToken generateJwtToken, IAuthService authService)
        {
            _generateJwtToken = generateJwtToken;
            _authService = authService;
        }

        /// <summary>
        /// Serviço para login de usuário registrado no sistema. 
        /// </summary>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<AuthenticationResponseModel> Login([FromBody] LoginRequestModel user)
        {
            var auth = _generateJwtToken.GenerateToken(user).Result;
            if (auth is null)
            {
                NotFoundException.Throw("006", "Usuário e/ou senha incorretos.");
            }

            return Ok(auth);
        }

        /// <summary>
        /// Serviço para registro de novo usuário no sistema.
        /// </summary>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedErrorModel), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult<AuthenticationResponseModel> Register([FromBody] RegisterModel user)
        {
            var result = _authService.Register(user);
            return Ok(result);
        }
    }
}