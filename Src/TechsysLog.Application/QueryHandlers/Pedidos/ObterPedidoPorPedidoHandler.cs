using System;
using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Application.QueryHandlers.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Pedidos
{
    /// <summary>
    /// Handler responsável por processar a query de busca de um pedido específico através do seu número.
    /// </summary>
    public class ObterPedidoPorPedidoHandler : IQueryHandler<ObterPedidoPorNumeroQuery, PedidoDto>
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterPedidoPorPedidoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public ObterPedidoPorPedidoHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Executa a consulta para localizar um pedido pelo número informado com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta contendo o número do pedido.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Um objeto <see cref="PedidoDto"/> com os detalhes do pedido localizado.</returns>
        /// <exception cref="InvalidOperationException">Lançada caso o pedido não seja encontrado na base de dados.</exception>
        public async Task<PedidoDto> HandleAsync(ObterPedidoPorNumeroQuery query, CancellationToken ct)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorNumeroAsync(query.NumeroPedido, ct);

                if (pedido is null)
                    throw new InvalidOperationException("Pedido não encontrado.");

                return new PedidoDto
                {
                    Id = pedido.Id,
                    NumeroPedido = pedido.NumeroPedido,
                    Status = pedido.Status,
                    UsuarioId = pedido.UsuarioId,
                    ValorTotal = pedido.ValorTotal,
                    DataCriacao = pedido.DataCriacao,
                    DataAtualizacao = pedido.DataAtualizacao,
                    Descricao = pedido.Descricao,
                    Itens = pedido.Itens.ToList()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}