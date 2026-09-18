using LocadoraCarros.Data.Dtos;
using LocadoraCarros.Model;
using LocadoraCarros.Model.Context;
using LocadoraCarros.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository 
    {
        private MySQLContext _context;

        public ClienteRepository(MySQLContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetById(long id)
        {
            return await _context.Clientes.FindAsync(id);
        }

/*        
        public async Task<Cliente?> Add(Cliente cliente)
        {

            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> Update(Cliente cliente)
        {
            var clienteLocadora = await _context.Clientes
                .FirstOrDefaultAsync(c => c.id == cliente.id);

            if (clienteLocadora == null)
            {
                return null;
            }

            clienteLocadora.Nome = cliente.Nome;
            clienteLocadora.Email = cliente.Email;
            clienteLocadora.Telefone = cliente.Telefone;

            await _context.SaveChangesAsync();

            return clienteLocadora;
        }

        public async Task Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }
*/


    }
}
