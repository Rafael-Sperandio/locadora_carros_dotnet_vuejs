using LocadoraCarros.Data.Dtos.Locacao;
using LocadoraCarros.Model;
using LocadoraCarros.Models;

namespace LocadoraCarros.Services.Interface
{
    public interface ILocacaoService 
    {

        Task<IEnumerable<ResponseLocacaoDto>> GetAll();
        
        Task<ResponseLocacaoDto> GetById(long id, bool includeCarro, bool includeCliente);

        Task<IEnumerable<ResponseLocacaoDto>> GetByCliente(long clienteId, bool includeCarro, bool includeCliente);

        Task<IEnumerable<ResponseLocacaoDto>> GetByCarro(long carroId, bool includeCarro , bool includeCliente);

        Task<ResponseLocacaoDto?> Create(CreateLocacaoDto dto);

        Task<ResponseLocacaoDto?> Update(long id, UpdateLocacaoDto dto,bool atualizarPreco);

        //TODO talvez regra para deletar apenas locacao canceladas ou finalziadas
        Task<ResponseLocacaoDto> DeleteById(long id);

        Task<ResponseLocacaoDto?> SetStatusCancelar(long id);

        Task<ResponseLocacaoDto?> SetStatusFinalizar(long id);



    }
}
