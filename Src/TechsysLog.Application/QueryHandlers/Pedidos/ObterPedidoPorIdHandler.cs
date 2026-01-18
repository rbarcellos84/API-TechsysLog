using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.DTOs;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Pedidos
{
    /// <summary>
    /// Handler responsável por processar a query de busca de um pedido específico através do seu id.
    /// </summary>
    public class ObterPedidoPorIdHandler
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterPedidoPorIdQuery"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public ObterPedidoPorIdHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Executa a consulta para localizar um pedido pelo id informado com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta contendo o id do pedido.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Um objeto <see cref="PedidoDto"/> com os detalhes do pedido localizado.</returns>
        /// <exception cref="InvalidOperationException">Lançada caso o pedido não seja encontrado na base de dados.</exception>
        public async Task<PedidoDto> HandleAsync(ObterPedidoPorIdQuery query, CancellationToken ct)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(query.Id, ct);

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
                DataEnvio = pedido.DataEnvio,
                Lida = pedido.Lida,
                DataAtualizacao = pedido.DataAtualizacao,
                Descricao = pedido.Descricao,
                Itens = pedido.Itens.ToList(),

                EnderecoEntrega = pedido.EnderecoEntrega == null ? new EnderecoDto() : new EnderecoDto
                {
                    CEP = pedido.EnderecoEntrega.CEP,
                    Rua = pedido.EnderecoEntrega.Rua,
                    Numero = pedido.EnderecoEntrega.Numero,
                    Bairro = pedido.EnderecoEntrega.Bairro,
                    Cidade = pedido.EnderecoEntrega.Cidade,
                    Estado = pedido.EnderecoEntrega.Estado
                }
            };
        }
    }
}
