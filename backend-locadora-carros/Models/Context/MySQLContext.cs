using LocadoraCarros.Models;
using LocadoraCarrosBackEnd.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Model.Context
{
    public class MySQLContext : IdentityDbContext<
        ApplicationUser,
        IdentityRole<long>,
        long>
    {
        public MySQLContext(
            DbContextOptions<MySQLContext> options)
            : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set;}

        public DbSet<Carro> Carros { get; set; }

        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

/*            modelBuilder.Entity<ApplicationUser>()
                .HasOne(user => user.Usuario)
                .WithOne()
                .HasForeignKey<ApplicationUser>(user => user.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);*/

/*            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                id = 1,
                Nome = "Moacir Carvalho",
                Email= "moacir@teste.com",
                Telefone= "+55 21 994132-XXXX"
            });*/

        }

    }
}