using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.DTOs;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Application.QueryHandlers.Interfaces;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Pedidos
{
    public class ObterListaPedidosPorEstadoHandler : IQueryHandler<ObterListaPedidosPorEstadoQuery, IEnumerable<PedidoDto>>
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterListaPedidosPorEstadoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public ObterListaPedidosPorEstadoHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Executa a consulta para listar os pedidos por estado com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta para listagem de pedidos.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma coleção de <see cref="PedidoDto"/> contendo todos os pedidos filtrados por estados.</returns>
        public async Task<IEnumerable<PedidoDto>> HandleAsync(ObterListaPedidosPorEstadoQuery query, CancellationToken ct)
        {
            try
            {
                var pedidos = await _pedidoRepository.ListarTodosPorEstadoAsync(query.Status, ct);

                return pedidos.Select(p => new PedidoDto
                {
                    Id = p.Id,
                    NumeroPedido = p.NumeroPedido,
                    Status = p.Status,
                    UsuarioId = p.UsuarioId,
                    ValorTotal = p.ValorTotal,
                    DataCriacao = p.DataCriacao,
                    DataEnvio = p.DataEnvio,
                    Lida = p.Lida,
                    DataAtualizacao = p.DataAtualizacao,
                    Descricao = p.Descricao,
                    Itens = p.Itens.ToList(),

                    EnderecoEntrega = new EnderecoDto
                    {
                        CEP = p.EnderecoEntrega.CEP,
                        Rua = p.EnderecoEntrega.Rua,
                        Numero = p.EnderecoEntrega.Numero,
                        Bairro = p.EnderecoEntrega.Bairro,
                        Cidade = p.EnderecoEntrega.Cidade,
                        Estado = p.EnderecoEntrega.Estado
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
