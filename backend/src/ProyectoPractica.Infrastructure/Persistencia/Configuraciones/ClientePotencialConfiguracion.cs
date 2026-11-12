using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoPractica.Domain.ClientesPotenciales;

namespace ProyectoPractica.Infrastructure.Persistencia.Configuraciones;

internal sealed class ClientePotencialConfiguracion : IEntityTypeConfiguration<ClientePotencial>
{
    public void Configure(EntityTypeBuilder<ClientePotencial> builder)
    {
        builder.ToTable("ClientesPotenciales");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nombre).HasMaxLength(120).IsRequired();
        builder.Property(c => c.Producto).HasConversion<string>().HasMaxLength(20);
        builder.Property(c => c.Email).HasMaxLength(254).IsRequired();
        builder.Property(c => c.Telefono).HasMaxLength(20).IsRequired();
    }
}
