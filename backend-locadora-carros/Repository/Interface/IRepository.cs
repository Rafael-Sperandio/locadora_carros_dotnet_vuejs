using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Models;

namespace LocadoraCarros.Repository.Interface
{
    public interface IRepository<T> where T : class 
    {
        //Add
        Task<T?> Add(T entity);

        Task<T?> Update(T entity);

        Task Delete(T entity);

        Task<bool> SaveChangesAsync();

    }
}
