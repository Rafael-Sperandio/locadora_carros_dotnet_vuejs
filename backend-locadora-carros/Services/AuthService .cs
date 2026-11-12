using LocadoraCarrosBackEnd.Data.Dtos.Auth;
using LocadoraCarrosBackEnd.Data.Dtos.Usuario;
using LocadoraCarrosBackEnd.Models.Identity;
using LocadoraCarrosBackEnd.Services.Interface;
using LocadoraCarrosBackEnd.Utils.Identity;
using Microsoft.AspNetCore.Identity;

namespace LocadoraCarrosBackEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<IdentityResult> Register(
            RegisterUsuarioDto model)
        {
            var user = new ApplicationUser
            {
                Nome = model.Nome,
                UserName = model.Email,
                Email = model.Email,
                Telefone = model.Telefone,
                CNH = model.CNH,
                EnderecoCompleto = "Endereço não cadastrado"
            };

            var result = await _userManager.CreateAsync(
                user,
                model.Senha 
            );

            if (!result.Succeeded)
                return result;

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                UserRoles.Cliente
            );

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                return roleResult;
            }

            return IdentityResult.Success;
        }

        public async Task<LoginResponseDto?> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(
                model.Email
            );

            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Senha,
                lockoutOnFailure: true
            );

            if (!result.Succeeded)
                return null;

            var token = await _jwtService.GenerateToken(user);

            var roles = await _userManager.GetRolesAsync(user);

            var usuario = new ResponseUsuarioDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Telefone = user.Telefone,
                Roles = roles
            };

            return new LoginResponseDto
            {
                Token = token,
                DataExpiracao = DateTime.UtcNow.AddHours(2),
                Usuario = usuario
            };
        }
    }
}