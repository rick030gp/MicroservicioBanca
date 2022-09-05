using MicroservicioBanca.Domain;
using MicroservicioBanca.Domain.Movimientos;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Cuentas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MicroservicioBanca.Repository.Cuentas.Configuration
{
    public class MovimientoEfConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable($"{MicroservicioBancaDbProperties.DbTablePrefix}{MicroservicioBancaDbProperties.TableNameMovement}", MicroservicioBancaDbProperties.DbSchema);
            builder.Property(movimiento => movimiento.Fecha).HasDefaultValueSql("getdate()");
            builder.Property(movimiento => movimiento.Tipo).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxMovementTypeLength)
                .HasConversion(v => v.ToString(), v => (TipoMovimiento)Enum.Parse(typeof(TipoMovimiento), v));
            builder.HasKey(movimiento => movimiento.Id);
        }
    }
}
