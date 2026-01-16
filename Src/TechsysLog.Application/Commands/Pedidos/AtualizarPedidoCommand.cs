using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Commands.Pedidos;

/// <summary>
/// Comando para atualizar os dados de um pedido existente.
/// </summary>
public class AtualizarPedidoCommand : ICommand
{
    /// <summary>
    /// Identificador único do pedido.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Número atualizado do pedido.
    /// </summary>
    public string Numero { get; }

    /// <summary>
    /// Lista de itens atualizada do pedido.
    /// </summary>
    public IReadOnlyCollection<string> Itens { get; }

    /// <summary>
    /// Valor total atualizado do pedido.
    /// </summary>
    public decimal ValorTotal { get; }

    /// <summary>
    /// Status atualizado do pedido (ex.: Pendente, Processando, Concluído, Cancelado).
    /// </summary>
    public Status Status { get; }

    /// <summary>
    /// Construtor do comando de atualização de pedido.
    /// </summary>
    /// <param name="id">Identificador único do pedido.</param>
    /// <param name="numero">Número atualizado do pedido.</param>
    /// <param name="itens">Lista de itens atualizada.</param>
    /// <param name="valorTotal">Valor total atualizado.</param>
    /// <param name="status">Status atualizado do pedido.</param>
    public AtualizarPedidoCommand(Guid id, string numero, IEnumerable<string> itens, decimal valorTotal, Status status)
    {
        Id = id;
        Numero = numero;
        Itens = itens.ToList().AsReadOnly();
        ValorTotal = valorTotal;
        Status = status;
    }
}
