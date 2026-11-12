using ProyectoPractica.Application.Abstracciones.Mensajeria;

namespace ProyectoPractica.Application.ClientesPotenciales.Commands.EliminarClientePotencial;

public sealed record EliminarClientePotencialCommand(int Id) : ICommand<Unidad>;
