using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Model.Context;
using LocadoraCarros.Models;
using LocadoraCarros.Models.Enums;
using LocadoraCarros.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Repository
{
    public class LocacaoRepository : Repository<Locacao>, ILocacaoRepository
    {
        private MySQLContext _context;

        public LocacaoRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locacao>> GetAll(
            bool includeCarro = false, 
            bool includeCliente = false)
        {
            var query = _context.Locacoes.AsQueryable();

            if (includeCarro)
                query = query.Include(l => l.Carro);

            if (includeCliente)
                query = query.Include(l => l.Cliente);

            return await query.ToListAsync();
        }

        public async Task<Locacao?> GetById(
            long id,
            bool includeCarro = false,
            bool includeCliente = false)
        {
            var query = _context.Locacoes.AsQueryable();

            if (includeCarro)
                query = query.Include(l => l.Carro);

            if (includeCliente)
                query = query.Include(l => l.Cliente);

            return await query.FirstOrDefaultAsync(l => l.id == id);
        }

        public async Task<IEnumerable<Locacao>> GetByCarro(long carroId)
        {
            //precisa do await?
            return await _context.Locacoes.Where(l => l.CarroId == carroId).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByCliente(long clienteId)
        {
            return await _context.Locacoes.Where(l => l.ClienteId == clienteId).ToListAsync();
        }


        public async Task<bool> CarroPossuiLocacaoNoPeriodo(
            long carroId,
            DateTime inicio,
            DateTime fim, 
            long? locacaoId = null)
        {
            return  _context.Locacoes.Where(l => 
                l.CarroId == carroId  
                && (l.Status != StatusLocacao.Finalizada 
                && l.Status != StatusLocacao.Cancelada)
                &&(inicio <= l.DataFim
                && fim >= l.DataInicio)
                &&(locacaoId == null || l.id != locacaoId)
            ).Any();
        }



    }
}
