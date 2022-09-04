using MicroservicioBanca.Domain;
using MicroservicioBanca.Domain.Cuentas;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Cuentas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MicroservicioBanca.Repository.Cuentas.Configuration
{
    public class CuentaEfConfiguration : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable($"{MicroservicioBancaDbProperties.DbTablePrefix}{MicroservicioBancaDbProperties.TableNameAccount}", MicroservicioBancaDbProperties.DbSchema);
            builder.Property(cuenta => cuenta.NumeroCuenta).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxAccountNumberLength);
            builder.Property(cuenta => cuenta.TipoCuenta).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxAccountTypeLength)
                .HasConversion(v => v.ToString(), v => (TipoCuenta)Enum.Parse(typeof(TipoCuenta), v));
            builder.Property(cuenta => cuenta.NumeroCuenta).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxAccountNumberLength);
            builder.HasKey(cuenta => cuenta.Id);
            builder.HasMany(cuenta => cuenta.Movimientos)
                .WithOne()
                .HasForeignKey(cuenta => cuenta.CuentaId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasIndex(cuenta => cuenta.NumeroCuenta).IsUnique();
        }
    }
}
