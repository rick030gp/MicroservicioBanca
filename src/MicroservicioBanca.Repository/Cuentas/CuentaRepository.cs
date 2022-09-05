using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Repository.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MicroservicioBanca.Repository.Cuentas
{
    public class CuentaRepository : GenericRepository<Cuenta, Guid>, ICuentaRepository
    {
        private readonly IMicroservicioBancaDbContext _context;
        public CuentaRepository(IMicroservicioBancaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cuenta> GetByAccountNumberAsync(string accountNumber)
        {
            return await _context.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == accountNumber);
        }

        public async Task<Cuenta> GetWithMovementsByAccountNumberAsync(string accountNumber)
        {
            return await _context.Cuentas.Include(c => c.Movimientos).FirstOrDefaultAsync(c => c.NumeroCuenta == accountNumber);
        }
    }
}
