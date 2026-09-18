using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Models;

namespace LocadoraCarros.Repository.Interface
{
    public interface ICarroRepository : IRepository<Carro>
    {
        Task<IEnumerable<Carro>> GetAll();

        Task<Carro> GetById(long id);
/*
        Task<Carro?> Add(Carro carro);

        Task<Carro?> Update(Carro carro);


        Task Delete(Carro carro);

*/

    }
}
