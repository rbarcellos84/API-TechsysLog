using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Pedidos
{
    /// <summary>
    /// Query para obter um pedido específico pelo número do pedido.
    /// Retorna um objeto <see cref="PedidoDto"/> correspondente ao pedido encontrado.
    /// </summary>
    public class ObterPedidoPorNumeroQuery : IQuery<PedidoDto>
    {
        /// <summary>
        /// Número único do pedido que será consultado.
        /// </summary>
        public string NumeroPedido { get; }

        /// <summary>
        /// Construtor da query para obter pedido por número.
        /// </summary>
        /// <param name="numeroPedido">Número do pedido.</param>
        public ObterPedidoPorNumeroQuery(string numeroPedido)
        {
            NumeroPedido = numeroPedido;
        }
    }
}
