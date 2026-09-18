using LocadoraCarros.Models;

namespace LocadoraCarros.Repository.Interface
{
    public interface ILocacaoRepository : IRepository<Locacao>
    {
        Task<IEnumerable<Locacao>> GetAll();

        Task<Locacao?> GetById(long id);

        Task<IEnumerable<Locacao>> GetByCliente(long clienteId);

        Task<IEnumerable<Locacao>> GetByCarro(long carroId);

        //forneca o locacaoId em caso de atualização
        Task<bool> CarroPossuiLocacaoNoPeriodo(
            long carroId,
            DateTime inicio,
            DateTime fim,
            long? locacaoId = null);

    }
}
