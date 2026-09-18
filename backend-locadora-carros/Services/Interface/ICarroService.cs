using LocadoraCarros.Data.Dtos.Carro;
using LocadoraCarros.Model;
using LocadoraCarros.Repository.Interface;

namespace LocadoraCarros.Services.Interface
{
    public interface ICarroService 
    {
        Task<IEnumerable<ResponseCarroDto>> GetAll();
        Task<ResponseCarroDto> GetById(long id);


        Task<ResponseCarroDto?> Create(CreateCarroDto dto);

        Task<ResponseCarroDto?> Update(long id, UpdateCarroDto dto);

        Task<ResponseCarroDto> DeleteById(long id);
    }
}
