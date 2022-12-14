using MicroservicioBanca.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicioBanca.Clientes
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

        public async Task<Cliente> GetWithAccountsByIdentificationAndDatesAsync(string identification, DateTime fechaInicial, DateTime? fechaFinal = null)
        {
            var fechaFin = fechaFinal ?? DateTime.Now;
            return await _context.Clientes
                .Include(c => c.Cuentas)
                .ThenInclude(c => c.Movimientos.Where(
                    m => m.Fecha >= fechaInicial.Date && m.Fecha <= fechaFin.Date.AddSeconds(86399))
                    .OrderByDescending(m => m.Fecha))
                .AsSplitQuery()
                .OrderBy(c => c.Identificacion)
                .FirstOrDefaultAsync(c => c.Identificacion == identification);
        }
    }
}
