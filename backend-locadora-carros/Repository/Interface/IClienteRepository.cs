using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Models;

namespace LocadoraCarros.Repository.Interface
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetAll();

        Task<Cliente> GetById(long id);
/*
        Task<Cliente?> Add(Cliente cliente);

        Task<Cliente?> Update(Cliente cliente);

        Task Delete(Cliente cliente);

*/

    }
}
