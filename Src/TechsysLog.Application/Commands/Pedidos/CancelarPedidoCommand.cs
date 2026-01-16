using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Commands.Pedidos;

/// <summary>
/// Comando para cancelar um pedido existente.
/// </summary>
public class CancelarPedidoCommand : ICommand
{
    /// <summary>
    /// Identificador único do pedido que será cancelado.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Motivo do cancelamento do pedido.
    /// </summary>
    public string Motivo { get; }

    /// <summary>
    /// Construtor do comando de cancelamento de pedido.
    /// </summary>
    /// <param name="id">Identificador único do pedido.</param>
    /// <param name="motivo">Motivo do cancelamento.</param>
    public CancelarPedidoCommand(Guid id, string motivo)
    {
        Id = id;
        Motivo = motivo;
    }
}
