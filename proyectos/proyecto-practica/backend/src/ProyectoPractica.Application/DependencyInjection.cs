using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProyectoPractica.Application.Abstracciones.Mensajeria;
using ProyectoPractica.Application.Comun.Comportamientos;
using ProyectoPractica.Application.Comun.Mensajeria;

namespace ProyectoPractica.Application;

public static class DependencyInjection
{
    /// <summary>Registra el mediador, sus comportamientos, todos los manejadores y los validadores de la capa.</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var ensamblado = typeof(DependencyInjection).Assembly;

        services.AddScoped<IMediador, Mediador>();
        services.AddScoped(typeof(IComportamientoPeticion<,>), typeof(ComportamientoValidacion<,>));
        services.AddValidatorsFromAssembly(ensamblado, ServiceLifetime.Scoped, includeInternalTypes: true);

        // Cada clase concreta que implemente IManejadorPeticion<,> se registra por esa interfaz.
        var manejadores =
            from tipo in ensamblado.GetTypes()
            where tipo is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
            from interfaz in tipo.GetInterfaces()
            where interfaz.IsGenericType && interfaz.GetGenericTypeDefinition() == typeof(IManejadorPeticion<,>)
            select (interfaz, tipo);

        foreach (var (interfaz, tipo) in manejadores)
        {
            services.AddScoped(interfaz, tipo);
        }

        return services;
    }
}
