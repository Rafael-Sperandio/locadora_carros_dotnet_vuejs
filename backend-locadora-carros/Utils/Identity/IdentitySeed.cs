using LocadoraCarrosBackEnd.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace LocadoraCarrosBackEnd.Utils.Identity
{
    public static class IdentitySeed
    {
        public static async Task SeedRolesAsync(
            RoleManager<IdentityRole<long>> roleManager)
        {
            string[] roles =
            {
                UserRoles.Administrador,
                UserRoles.Funcionario,
                UserRoles.Cliente
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole<long>(role));
                }
            }
        }

        public static async Task SeedAdminAsync(
          UserManager<ApplicationUser> userManager)
        {
            string email = "admin@locadora.com";

            var admin = await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Nome = "Admin",
                    Telefone = "+55 11 9XXXX-YYYY",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    admin,
                    "Admin@123456"
                );

                if (!result.Succeeded)
                {
                    var errors = string.Join(
                        "; ",
                        result.Errors.Select(e => e.Description)
                    );

                    throw new Exception(
                        $"Erro ao criar administrador: {errors}"
                    );
                }
            }

            if (!await userManager.IsInRoleAsync(
                admin, UserRoles.Administrador))
            {
                await userManager.AddToRoleAsync(
                    admin, UserRoles.Administrador);
            }
        }
    }
}
