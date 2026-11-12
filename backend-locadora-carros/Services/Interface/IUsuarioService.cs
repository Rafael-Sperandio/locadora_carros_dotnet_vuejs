using LocadoraCarrosBackEnd.Data.Dtos.Usuario;
using Microsoft.AspNetCore.Identity;

namespace LocadoraCarrosBackEnd.Services.Interface
{
    public interface IUsuarioService
    {
        Task<IList<ResponseUsuarioDto>> GetAll();

        Task<ResponseUsuarioDto?> GetById(long id);

        Task<IdentityResult> Create(CreateUsuarioDto model);

        Task<IdentityResult?> Update(long id, UpdateUsuarioDto model);

        Task<IdentityResult?> DeleteById(long id);
    }
}