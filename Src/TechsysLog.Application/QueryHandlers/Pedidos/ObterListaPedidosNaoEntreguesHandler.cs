using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Application.QueryHandlers.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Pedidos
{
    /// <summary>
    /// Handler responsável por processar a query de listagem de pedidos pendentes (não entregues).
    /// </summary>
    public class ObterListaPedidosNaoEntreguesHandler
        : IQueryHandler<ObterListaPedidosNaoEntreguesQuery, IEnumerable<PedidoDto>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterListaPedidosNaoEntreguesHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public ObterListaPedidosNaoEntreguesHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Executa a consulta para recuperar pedidos pendentes e realiza o mapeamento para DTO com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta para pedidos não entregues.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma coleção de <see cref="PedidoDto"/> representando os pedidos em andamento.</returns>
        public async Task<IEnumerable<PedidoDto>> HandleAsync(ObterListaPedidosNaoEntreguesQuery query, CancellationToken ct)
        {
            try
            {
                var pedidos = await _pedidoRepository.ListarTodosNaoEntregueAsync(ct);

                return pedidos.Select(p => new PedidoDto
                {
                    Id = p.Id,
                    NumeroPedido = p.NumeroPedido,
                    Status = p.Status,
                    UsuarioId = p.UsuarioId,
                    ValorTotal = p.ValorTotal,
                    DataCriacao = p.DataCriacao,
                    DataAtualizacao = p.DataAtualizacao,
                    Descricao = p.Descricao,
                    Itens = p.Itens.ToList()
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}