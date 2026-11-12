using LocadoraCarrosBackEnd.Data.Dtos.Auth;
using LocadoraCarrosBackEnd.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraCarrosBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUsuarioDto model)
        {
            var result = await _authService.Register(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                mensagem = "Usuário cadastrado com sucesso."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var response = await _authService.Login(model);

            if (response == null)
                return Unauthorized(new
                {
                    mensagem = "E-mail ou senha inválidos."
                });

            return Ok(response);
        }
    }
}