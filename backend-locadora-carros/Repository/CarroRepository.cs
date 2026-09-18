using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Model.Context;
using LocadoraCarros.Models;
using LocadoraCarros.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Repository
{

 


    public class CarroRepository : Repository<Carro> , ICarroRepository
    {
        private MySQLContext _context;

        public CarroRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Carro>> GetAll()
        {
            return await _context.Carros.ToListAsync();
        }

        public async Task<Carro> GetById(long id)
        {
            return await _context.Carros.FindAsync(id);
        }


/*
        public async Task<Carro?> Add(Carro Carro)
        {

            await _context.AddAsync(Carro);
            await _context.SaveChangesAsync();
            return Carro;
        }

        public async Task<Carro?> Update(Carro carroNovo)
        {
            var carroAntigo = await _context.Carros
                .FirstOrDefaultAsync(c => c.id == carroNovo.id);

            if (carroAntigo == null)
            {
                return null;
            }
            carroAntigo.Categoria = carroNovo.Categoria;
            carroAntigo.Ano = carroNovo.Ano;
            carroAntigo.Placa = carroNovo.Placa;
            carroAntigo.Status = carroNovo.Status;
            carroAntigo.ValorDiaria = carroNovo.ValorDiaria;

            await _context.SaveChangesAsync();

            return carroAntigo;
        }

        public async Task Delete(Carro Carro)
        {
            _context.Carros.Remove(Carro);

            await _context.SaveChangesAsync();
        }

*/
    }
}
