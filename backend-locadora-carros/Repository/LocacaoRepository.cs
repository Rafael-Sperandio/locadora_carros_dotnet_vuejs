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

        public async Task<IEnumerable<Locacao>> GetAll()
        {
            return await _context.Locacoes.ToListAsync();
        }
        public async Task<Locacao?> GetById(long id)
        {
            return await _context.Locacoes.FindAsync(id);
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


        //verifica se exite alguma locacao nao StatusLocacao.Finalizada ou StatusLocacao.Cancelada
        // que a 
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
                &&(locacaoId == null ||
                (locacaoId != null 
                && l.id != locacaoId))
           ).Any();
        }



    }
}
