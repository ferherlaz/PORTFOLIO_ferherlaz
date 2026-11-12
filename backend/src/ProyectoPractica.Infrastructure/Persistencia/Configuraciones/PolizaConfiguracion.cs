using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoPractica.Domain.Polizas;

namespace ProyectoPractica.Infrastructure.Persistencia.Configuraciones;

internal sealed class PolizaConfiguracion : IEntityTypeConfiguration<Poliza>
{
    public void Configure(EntityTypeBuilder<Poliza> builder)
    {
        builder.ToTable("Polizas");
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.NumPoliza).IsUnique();
        builder.HasIndex(p => p.FechaVencimiento);

        builder.Property(p => p.NombreRiesgo).HasMaxLength(120).IsRequired();
        builder.Property(p => p.Estado).HasConversion<string>().HasMaxLength(20);

        // Value objects como "complex types": se guardan en columnas de la propia tabla y se sustituyen enteros.
        // Sus propiedades no tienen setter, por eso se declaran explícitamente.
        builder.ComplexProperty(p => p.Titular, t =>
        {
            t.Property(x => x.Cif).HasColumnName("TitularCif").HasMaxLength(20);
            t.Property(x => x.Nombre).HasColumnName("TitularNombre").HasMaxLength(120);
            t.Property(x => x.DirFiscal).HasColumnName("TitularDirFiscal").HasMaxLength(200);
        });

        builder.ComplexProperty(p => p.Facturacion, f =>
        {
            f.Property(x => x.DirFacturacion).HasColumnName("FacturacionDireccion").HasMaxLength(200);
            f.Property(x => x.Numero).HasColumnName("FacturacionNumero").HasMaxLength(10);
            f.Property(x => x.Ciudad).HasColumnName("FacturacionCiudad").HasMaxLength(80);
            f.Property(x => x.Cp).HasColumnName("FacturacionCp").HasMaxLength(5);
        });

        builder.ComplexProperty(p => p.Pago, p =>
        {
            p.Property(x => x.Nombre).HasColumnName("PagoNombre").HasMaxLength(120);
            p.Property(x => x.Cif).HasColumnName("PagoCif").HasMaxLength(20);
            p.Property(x => x.TipoCobro).HasColumnName("PagoTipoCobro").HasMaxLength(60);
            p.Property(x => x.Iban).HasColumnName("PagoIban").HasMaxLength(34);
            p.Property(x => x.Banco).HasColumnName("PagoBanco").HasMaxLength(80);
        });

        builder.ComplexProperty(p => p.Correspondencia, c =>
        {
            c.Property(x => x.Datos).HasColumnName("CorrespondenciaDatos").HasMaxLength(120);
            c.Property(x => x.Poblacion).HasColumnName("CorrespondenciaPoblacion").HasMaxLength(80);
            c.Property(x => x.Cp).HasColumnName("CorrespondenciaCp").HasMaxLength(5);
            c.Property(x => x.Direccion).HasColumnName("CorrespondenciaDireccion").HasMaxLength(200);
            c.Property(x => x.Numero).HasColumnName("CorrespondenciaNumero").HasMaxLength(10);
            c.Property(x => x.Aclarador).HasColumnName("CorrespondenciaAclarador").HasMaxLength(60);
        });

        builder.ComplexProperty(p => p.Contacto, c =>
        {
            c.Property(x => x.CorreoPrincipal).HasColumnName("ContactoCorreoPrincipal").HasMaxLength(254);
            c.Property(x => x.CorreoSecundario).HasColumnName("ContactoCorreoSecundario").HasMaxLength(254);
            c.Property(x => x.Telefono).HasColumnName("ContactoTelefono").HasMaxLength(20);
        });

        builder.HasMany(p => p.Cuotas)
            .WithOne()
            .HasForeignKey(c => c.PolizaId)
            .OnDelete(DeleteBehavior.Cascade);

        // La colección se expone de solo lectura; EF trabaja con el campo privado _cuotas.
        builder.Navigation(p => p.Cuotas).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
