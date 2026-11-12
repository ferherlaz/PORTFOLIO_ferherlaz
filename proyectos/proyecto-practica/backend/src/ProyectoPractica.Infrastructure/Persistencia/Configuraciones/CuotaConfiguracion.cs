using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Infrastructure.Persistencia.Configuraciones;

internal sealed class CuotaConfiguracion : IEntityTypeConfiguration<Cuota>
{
    public void Configure(EntityTypeBuilder<Cuota> builder)
    {
        builder.ToTable("Cuotas");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.PolizaId, c.FechaEmision });
        builder.Property(c => c.Estado).HasConversion<string>().HasMaxLength(20);
    }
}
