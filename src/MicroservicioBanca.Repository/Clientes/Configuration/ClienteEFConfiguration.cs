using MicroservicioBanca.Domain;
using MicroservicioBanca.Domain.Clientes;
using MicroservicioBanca.Domain.Shared;
using MicroservicioBanca.Domain.Shared.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MicroservicioBanca.Repository.Clientes.Configuration
{
    public class ClienteEFConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable($"{MicroservicioBancaDbProperties.DbTablePrefix}{MicroservicioBancaDbProperties.TableNameClient}", MicroservicioBancaDbProperties.DbSchema);
            builder.Property(cliente => cliente.Nombre).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxNameLength);
            builder.Property(cliente => cliente.Genero).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxGenderLength)
                .HasConversion(v => v.ToString(), v => (Genero)Enum.Parse(typeof(Genero), v));
            builder.Property(cliente => cliente.Identificacion).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxIdentificationLength);
            builder.Property(cliente => cliente.Direccion).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxHomeAddressLength);
            builder.Property(cliente => cliente.Telefono).HasMaxLength(MicroservicioBancaConsts.MaxPhoneLength);
            builder.Property(cliente => cliente.Contrasenia).IsRequired()
                .HasMaxLength(MicroservicioBancaConsts.MaxPasswordLength);
            builder.HasKey(cliente => cliente.Id);
            builder.HasMany(cliente => cliente.Cuentas)
                .WithOne()
                .HasForeignKey(cliente => cliente.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasIndex(cliente => cliente.Identificacion).IsUnique();
        }
    }
}
