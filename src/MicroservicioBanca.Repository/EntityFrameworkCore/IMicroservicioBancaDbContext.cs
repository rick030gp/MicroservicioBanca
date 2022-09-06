using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Movimientos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicroservicioBanca.Repository.EntityFrameworkCore
{
    public interface IMicroservicioBancaDbContext
    {
        DbSet<Cliente> Clientes { get; set; }
        DbSet<Cuenta> Cuentas { get; set; }
        DbSet<Movimiento> Movimientos { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void RemoveRange(IEnumerable<object> entities);
        EntityEntry Update(object entity);
    }
}
