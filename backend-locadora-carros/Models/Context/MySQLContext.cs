using LocadoraCarros.Models;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Model.Context
{
    public class MySQLContext : DbContext
    {
        // MySQLContext() {}
        public MySQLContext(DbContextOptions<MySQLContext> options): base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set;}

        public DbSet<Carro> Carros { get; set; }

        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                id = 1,
                Nome = "Moacir Carvalho",
                Email= "moacir@teste.com",
                Telefone= "+55 21 994132-XXXX"
            });

        }
        //*/


    }
}