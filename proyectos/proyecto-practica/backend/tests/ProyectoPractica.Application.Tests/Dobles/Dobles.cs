using ProyectoPractica.Domain.ClientesPotenciales;
using ProyectoPractica.Domain.Polizas;
using ProyectoPractica.Domain.Repositorios;

namespace ProyectoPractica.Application.Tests.Dobles;

/// <summary>Repositorio en memoria para probar los casos de uso sin base de datos.</summary>
internal sealed class PolizaRepositoryFalso(params Poliza[] polizas) : IPolizaRepository
{
    private readonly Dictionary<int, Poliza> _polizas = polizas.Select((p, i) => (p, i)).ToDictionary(x => x.i + 1, x => x.p);

    public Task<Poliza?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        Task.FromResult(_polizas.GetValueOrDefault(id));
}

internal sealed class ClientePotencialRepositoryFalso(params ClientePotencial[] clientes) : IClientePotencialRepository
{
    private readonly Dictionary<int, ClientePotencial> _clientes = clientes.Select((c, i) => (c, i)).ToDictionary(x => x.i + 1, x => x.c);

    public IReadOnlyCollection<ClientePotencial> Restantes => _clientes.Values;

    public Task<ClientePotencial?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        Task.FromResult(_clientes.GetValueOrDefault(id));

    public void Eliminar(ClientePotencial cliente)
    {
        var clave = _clientes.Single(c => ReferenceEquals(c.Value, cliente)).Key;
        _clientes.Remove(clave);
    }
}

internal sealed class UnitOfWorkFalso : IUnitOfWork
{
    public int VecesGuardado { get; private set; }

    public Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default)
    {
        VecesGuardado++;
        return Task.FromResult(1);
    }
}

internal static class PolizasDePrueba
{
    public static Poliza Nueva() => new(
        numPoliza: 75_846_843,
        nombreRiesgo: "Seat León",
        fechaContrato: new DateOnly(2026, 1, 15),
        fechaVencimiento: new DateOnly(2027, 1, 15),
        importe: 1200m,
        estado: EstadoPoliza.Pendiente,
        titular: Titular.Crear("12345678Z", "José Martínez", "Calle Mayor 1, Madrid"),
        facturacion: Facturacion.Crear("Calle Mayor", "1", "Madrid", "28013"),
        pago: Pago.Crear("José Martínez", "12345678Z", "Al contado", "ES9121000418450200051332", "CaixaBank"),
        correspondencia: Correspondencia.Crear("José Martínez", "Madrid", "28013", "Calle Mayor", "1", "Bajo"),
        contacto: Contacto.Crear("jose@gmail.com", "", "+34 600 111 222"));
}
