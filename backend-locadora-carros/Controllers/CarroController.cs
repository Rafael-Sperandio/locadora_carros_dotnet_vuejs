using LocadoraCarros.Data.Dtos.Carro;
using LocadoraCarros.Model;
using LocadoraCarros.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace LocadoraCarros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarroController: ControllerBase
    {

        private readonly ICarroService _carroService;
        public CarroController(ICarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var carros = await _carroService.GetAll();

            if (!carros.Any())
                return NoContent();

            return Ok(carros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var carro = await _carroService.GetById(id);

            if (carro == null)
                return NotFound();

            return Ok(carro);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCarroDto model)
        {
            var carro = await _carroService.Create(model);
            return Ok(carro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            long id,
            UpdateCarroDto model)
        {
            var carro = await _carroService.Update(id, model);

            if (carro == null)
                return NotFound();

            return Ok(carro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            var carro = await _carroService.DeleteById(id);

            if (carro == null)
                return NotFound();

            //pode ser mudado para no contet
            //return NoContent();
            // no caso de no contet o retorno do delete deve ser um bool se foi deletado ou não
            return Ok(carro);
        }

    }
}
