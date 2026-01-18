using TechsysLog.Domain.Interfaces;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Commands.Pedidos;

/// <summary>
/// Comando responsável por transportar os dados necessários para a criação de um novo pedido no sistema.
/// </summary>
public class CriarPedidoCommand : ICommand
{
    /// <summary>
    /// Obtém ou define o identificador único do usuário que está realizando o pedido.
    /// </summary>
    public Guid UsuarioId { get; set; }

    /// <summary>
    /// Obtém ou define o número de identificação do pedido (ex: "123456").
    /// </summary>
    /// <example>123456</example>
    public string NumeroPedido { get; set; }

    /// <summary>
    /// Obtém ou define a descrição geral do pedido.
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Obtém ou define a lista de itens inclusos no pedido.
    /// </summary>
    public List<string> Itens { get; set; } = new();

    /// <summary>
    /// Obtém ou define o valor total financeiro do pedido.
    /// </summary>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Obtém ou define o endereço completo onde a entrega deve ser realizada.
    /// </summary>
    public Endereco EnderecoEntrega { get; set; }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="CriarPedidoCommand"/>.
    /// </summary>
    public CriarPedidoCommand() { }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="CriarPedidoCommand"/> com parâmetros.
    /// </summary>
    /// <param name="usuarioId">Identificador do usuário.</param>
    /// <param name="numero">Número do pedido.</param>
    /// <param name="descricao">Descrição do pedido.</param>
    /// <param name="itens">Coleção de itens do pedido.</param>
    /// <param name="valorTotal">Valor total da transação.</param>
    /// <param name="enderecoEntrega">Objeto contendo os dados do endereço.</param>
    public CriarPedidoCommand(Guid usuarioId, string numeroPedido, string descricao, IEnumerable<string> itens, decimal valorTotal, Endereco enderecoEntrega)
    {
        UsuarioId = usuarioId;
        NumeroPedido = numeroPedido;
        Descricao = descricao;
        Itens = itens.ToList();
        ValorTotal = valorTotal;
        EnderecoEntrega = enderecoEntrega;
    }
}