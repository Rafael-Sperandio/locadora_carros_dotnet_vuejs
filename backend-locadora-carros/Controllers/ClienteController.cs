using LocadoraCarros.Data.Dtos.Cliente;
using LocadoraCarros.Model;
using LocadoraCarros.Models;
using LocadoraCarros.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace LocadoraCarros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController: ControllerBase
    {

        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _clienteService.GetAll();

            if (!clientes.Any())
                return NoContent();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var cliente = await _clienteService.GetById(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteDto model)
        {
            var cliente = await _clienteService.Create(model);
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            long id,
            UpdateClienteDto model)
        {
            var cliente = await _clienteService.Update(id, model);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var cliente = await _clienteService.DeleteById(id);

            if (cliente == null)
                return NotFound();

            //pode ser mudado para no contet
            //return NoContent();
            // no caso de no contet o retorno do delete deve ser um bool se foi deletado ou não
            return Ok(cliente);
        }

    }
}
