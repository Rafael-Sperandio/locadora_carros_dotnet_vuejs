
using LocadoraCarros.Data.Dtos.Locacao;
using LocadoraCarros.Models;
using LocadoraCarros.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace LocadoraLocacaos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocacaoController: ControllerBase
    {

        private readonly ILocacaoService _locacaoService;
        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locacaos = await _locacaoService.GetAll();

            if (!locacaos.Any())
                return NoContent();

            return Ok(locacaos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var locacao = await _locacaoService.GetById(id);

            if (locacao == null)
                return NotFound();

            return Ok(locacao);
        }

        [HttpGet("carro/{carroid}")]
        public async Task<IActionResult> GetByCarroId(long carroid)
        {
            var locacaos = await _locacaoService.GetByCarro(carroid);

            if (locacaos == null || !locacaos.Any())
                return NoContent();

            return Ok(locacaos);
        }

        [HttpGet("cliente/{clienteid}")]
        public async Task<IActionResult> GetByClienteId(long clienteid)
        {
            var locacaos = await _locacaoService.GetByCliente(clienteid);

            if (locacaos==null || !locacaos.Any())
                return NoContent();

            return Ok(locacaos);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLocacaoDto model)
        {

            var locacao = await _locacaoService.Create(model);

            if (locacao == null)//quando não possui carro ou cliente
                    return NotFound("Cliente ou carro não encontrado.");
                return Ok(locacao);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            long id,
            UpdateLocacaoDto model,
            [FromQuery] bool atualizarPreco = false)
        {
            var locacao = await _locacaoService.Update(id, model, atualizarPreco);

            if (locacao == null)
                return NotFound("Cliente ou carro não encontrado.");

            return Ok(locacao);
        }

        [HttpPut("finalize/{id}")]
        public async Task<IActionResult> SetStatusFinalizar(long id)
        {
            var locacao = await _locacaoService.SetStatusFinalizar(id);

            if (locacao == null)
                return NotFound();

            return Ok(locacao);
        }
        [HttpPut("Cancel/{id}")]
        public async Task<IActionResult> SetStatusCancelar(long id)
        {
            var locacao = await _locacaoService.SetStatusCancelar(id);

            if (locacao == null)
                return NotFound();

            return Ok(locacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var locacao = await _locacaoService.DeleteById(id);

            if (locacao == null)
                return NotFound();

            //pode ser mudado para no contet
            //return NoContent();
            // no caso de no contet o retorno do delete deve ser um bool se foi deletado ou não
            return Ok(locacao);
        }

    }
}
