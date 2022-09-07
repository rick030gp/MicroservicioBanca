using MicroservicioBanca.Clientes;
using MicroservicioBanca.Cuentas;
using MicroservicioBanca.Movimientos;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MicroservicioBanca.EntityFrameworkCore
{
    public class MicroservicioBancaDbContext : DbContext, IMicroservicioBancaDbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        public MicroservicioBancaDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
