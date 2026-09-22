using LocadoraCarros.Models;

namespace LocadoraCarros.Repository.Interface
{
    public interface ILocacaoRepository : IRepository<Locacao>
    {
        Task<IEnumerable<Locacao>> GetAll(bool includeCarro = false, bool includeCliente = false);

        Task<Locacao?> GetById(long id, bool includeCarro = false, bool includeCliente = false);

        Task<IEnumerable<Locacao>> GetByCliente(long clienteId);

        Task<IEnumerable<Locacao>> GetByCarro(long carroId);

        //forneca o locacaoId em caso de atualização para ser ignorado
        Task<bool> CarroPossuiLocacaoNoPeriodo(
            long carroId,
            DateTime inicio,
            DateTime fim,
            long? locacaoId = null);

    }
}
