using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Repository.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Repository.Clientes
{
    public class ClienteRepository : GenericRepository<Cliente, Guid>, IClienteRepository
    {
        private readonly IMicroservicioBancaDbContext _context;
        public ClienteRepository(IMicroservicioBancaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cliente> GetByIdentificationAsync(string identification)
        {
            return await _context.Clientes.FirstOrDefaultAsync(
                c => c.Identificacion == identification);
        }
    }
}
