using LocadoraCarros.Services.Interface;
using LocadoraCarrosBackEnd.Data.Dtos.Usuario;
using LocadoraCarrosBackEnd.Services.Interface;
using LocadoraCarrosBackEnd.Utils.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocadoraCarrosBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = UserRoles.Administrador)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = UserRoles.Administrador)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _usuarioService.GetAll();

            if (!usuarios.Any())
                return NoContent();

            return Ok(usuarios);
        }

        //mudar para get my info
        //TODO mudar authorização talvez
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var usuarioId = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            if (usuarioId == null || usuarioId != id.ToString())
                return Forbid();

            var usuario = await _usuarioService.GetById(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [Authorize(Roles = UserRoles.Administrador)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateUsuarioDto model)
        {
            var result = await _usuarioService.Create(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new
            {
                mensagem = "Usuário cadastrado com sucesso."
            });
        }

        //usuario pode mudar ele mesmo
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            long id,
            UpdateUsuarioDto model)
        {
            var usuarioId = User.FindFirst(
                ClaimTypes.NameIdentifier
            )?.Value;

            var role = User.FindFirst(
                ClaimTypes.Role
            )?.Value;

            if ((usuarioId == null || usuarioId != id.ToString()) && role != UserRoles.Administrador )
                return Forbid();


            var result = await _usuarioService.Update(id, model);

            if (result == null)
                return NotFound();

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [Authorize(Roles = UserRoles.Administrador)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var usuarioLogado = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (usuarioLogado == id.ToString())
            {
                return BadRequest(new
                {
                    mensagem = "Não é permitido excluir a própria conta."
                });
            }

            var result = await _usuarioService.DeleteById(id);

            if (result == null)
                return NotFound();

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}