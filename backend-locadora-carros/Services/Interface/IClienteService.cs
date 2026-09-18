using LocadoraCarros.Data.Dtos.Cliente;
using LocadoraCarros.Model;

namespace LocadoraCarros.Services.Interface
{
    public interface IClienteService 
    {
        //mudar precisa ser DTO
        Task<IEnumerable<ResponseClienteDto>> GetAll();
        Task<ResponseClienteDto> GetById(long id);


        Task<ResponseClienteDto?> Create(CreateClienteDto dto);

        Task<ResponseClienteDto?> Update(long id, UpdateClienteDto dto);

        Task<ResponseClienteDto> DeleteById(long id);


    }
}
