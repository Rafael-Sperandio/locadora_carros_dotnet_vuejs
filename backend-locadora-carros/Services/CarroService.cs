using AutoMapper;
using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Data.Dtos.Carro;
using LocadoraCarros.Data.Dtos.Carro;
using LocadoraCarros.Data.Dtos.Cliente;
using LocadoraCarros.Model;
using LocadoraCarros.Models;
using LocadoraCarros.Models.Enums.Carro;
using LocadoraCarros.Repository;
using LocadoraCarros.Repository.Interface;
using LocadoraCarros.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IMapper _mapper;
        public CarroService( ICarroRepository carroRepository,
            IMapper mapper
            )
        {
            _carroRepository = carroRepository;;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ResponseCarroDto>> GetAll()
        {
            var carros = await _carroRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponseCarroDto>>(carros);
        }

        public async Task<ResponseCarroDto> GetById(long id)
        {
            var carro = await _carroRepository.GetById(id);
            return _mapper.Map<ResponseCarroDto>(carro); ;
        }

        /// <summary>
        /// ao criar um carro ele é  StatusCarro.Disponivel por padrão
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseCarroDto?> Create(CreateCarroDto dto)
        {

            var carro = _mapper.Map<Carro>(dto);
            carro.Status = StatusCarro.Disponivel;
            carro = await _carroRepository.Add(carro);
            await _carroRepository.SaveChangesAsync();
            return _mapper.Map<ResponseCarroDto>(carro);

        }


        public async Task<ResponseCarroDto?> Update(long id,
        UpdateCarroDto dto)

        {
            var carro = await _carroRepository.GetById(id);

            if (carro == null)
            {
                return null;
            }
            carro = _mapper.Map(dto, carro);

            carro = await _carroRepository.Update(carro);
            await _carroRepository.SaveChangesAsync();
            return _mapper.Map<ResponseCarroDto>(carro);
        }
    

        public async Task<ResponseCarroDto> DeleteById(long id)
        {
            var carro = await _carroRepository.GetById(id);

            if (carro == null)
            {
                return null;
            }
            var retorno = _mapper.Map<ResponseCarroDto>(carro);
            await _carroRepository.Delete(carro);
            await _carroRepository.SaveChangesAsync();
            return retorno;
        }

    }
}
