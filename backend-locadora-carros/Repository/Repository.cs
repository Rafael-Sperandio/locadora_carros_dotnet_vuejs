using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Model.Context;
using LocadoraCarros.Models;
using LocadoraCarros.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly MySQLContext _context;

        public Repository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.FromResult(entity);
        }

        public Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
