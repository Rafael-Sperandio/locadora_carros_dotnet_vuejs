
using LocadoraCarrosBackEnd.Data.Dtos.Usuario;
using LocadoraCarrosBackEnd.Models.Identity;
using LocadoraCarrosBackEnd.Services.Interface;
using LocadoraCarrosBackEnd.Utils.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarrosBackEnd.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public UsuarioService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //rever
        public async Task<IList<ResponseUsuarioDto>> GetAll()
        {
            var usuarios = await _userManager.Users.ToListAsync();

            var response = new List<ResponseUsuarioDto>();

            foreach (var usuario in usuarios)
            {
                response.Add(await MapToResponse(usuario));
            }

            return response;
        }

        public async Task<ResponseUsuarioDto?> GetById(long id)
        {
            var usuario = await _userManager.FindByIdAsync(
                id.ToString()
            );

            if (usuario == null)
                return null;

            return await MapToResponse(usuario);
        }

        public async Task<IdentityResult> Create(
            CreateUsuarioDto model)
        {
            var user = new ApplicationUser
            {
                Nome = model.Nome,
                UserName = model.Email,
                Email = model.Email,
                Telefone = model.Telefone
            };

            if (!RolePermitida(model.Role))
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Role inválida."
                    }
                );

            }

            var result = await _userManager.CreateAsync(
                user,
                model.Senha
            );

            if (!result.Succeeded)
                return result;

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                model.Role
            );

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                return roleResult;
            }

            return IdentityResult.Success;
        }


        public async Task<IdentityResult?> Update(
            long id,
            UpdateUsuarioDto model)
        {
            var usuario = await _userManager.FindByIdAsync(
                id.ToString()
            );

            if (usuario == null)
                return null;

            usuario.Nome = model.Nome;
            usuario.Telefone = model.Telefone;

            return await _userManager.UpdateAsync(usuario);
        }

        public async Task<IdentityResult?> DeleteById(long id)
        {
            var usuario = await _userManager.FindByIdAsync(
                id.ToString()
            );

            if (usuario == null)
                return null;

            return await _userManager.DeleteAsync(usuario);
        }

        //mudar para automaper
        private async Task<ResponseUsuarioDto> MapToResponse(
            ApplicationUser usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            return new ResponseUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Roles = roles
            };
        }

        private bool RolePermitida(string role)
        {
            var rolesPermitidas = new[]
            {
                UserRoles.Administrador,
                UserRoles.Funcionario,
                UserRoles.Cliente
            };

            return rolesPermitidas.Contains(role);

        }
    }
}