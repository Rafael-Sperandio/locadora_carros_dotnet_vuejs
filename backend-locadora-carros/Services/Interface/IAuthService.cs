using LocadoraCarrosBackEnd.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace LocadoraCarrosBackEnd.Services.Interface
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterUsuarioDto model);

        Task<LoginResponseDto?> Login(LoginDto model);
    }
}