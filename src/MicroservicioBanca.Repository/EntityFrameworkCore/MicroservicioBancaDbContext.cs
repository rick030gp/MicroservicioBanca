using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Cuentas;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MicroservicioBanca.Repository.EntityFrameworkCore
{
    public class MicroservicioBancaDbContext : DbContext, IMicroservicioBancaDbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        
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
