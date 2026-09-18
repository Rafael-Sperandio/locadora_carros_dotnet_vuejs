using AutoMapper;
using LocadoraCarros.Data.Dtos.Cliente;
using LocadoraCarros.Model;
using LocadoraCarros.Repository;
using LocadoraCarros.Repository.Interface;
using LocadoraCarros.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteService( IClienteRepository clienteRepository,
            IMapper mapper)
        {
            _clienteRepository = clienteRepository;;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ResponseClienteDto>> GetAll()
        {
            var clientes = await _clienteRepository.GetAll();
            return _mapper.Map<IEnumerable<ResponseClienteDto>>(clientes);
        }

        public async Task<ResponseClienteDto> GetById(long id)
        {
            var cliente = await _clienteRepository.GetById(id);
            return _mapper.Map<ResponseClienteDto>(cliente); 
        }

        
        public async Task<ResponseClienteDto?> Create(CreateClienteDto dto)
        {
            var cliente =  _mapper.Map<Cliente>(dto);
            cliente = await _clienteRepository.Add(cliente);
            await _clienteRepository.SaveChangesAsync();
            return _mapper.Map<ResponseClienteDto>(cliente);
        }


        public async Task<ResponseClienteDto?> Update(long id,
        UpdateClienteDto dto)

        {
            var cliente = await _clienteRepository.GetById(id);

            if (cliente == null)
            {
                return null;
            }

            _mapper.Map(dto, cliente);

            cliente = await _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();
            return _mapper.Map<ResponseClienteDto>(cliente);
        }



        public async Task<ResponseClienteDto> DeleteById(long id)
        {
            var cliente = await _clienteRepository.GetById(id);

            if (cliente == null)
            {
                return null;
            }

            var retorno = _mapper.Map<ResponseClienteDto>(cliente);
            await _clienteRepository.Delete(cliente);
            await _clienteRepository.SaveChangesAsync();
            return retorno;
        }

    }
}
