using System;
using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Pedidos
{
    /// <summary>
    /// Manipulador responsável por processar o comando de marcação de pedido como lido.
    /// Intermedia a comunicação entre a aplicação e o repositório de domínio.
    /// </summary>
    public class MarcarPedidoHandler : ICommandHandler<MarcarPedidoCommand>
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="MarcarPedidoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public MarcarPedidoHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Executa a lógica de negócio para marcar um pedido específico como lido no banco de dados.
        /// </summary>
        /// <param name="command">O comando contendo o número do pedido a ser atualizado.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando o pedido não é encontrado no sistema.</exception>
        public async Task HandleAsync(MarcarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorNumeroAsync(command.NumeroPedido, ct);

                if (pedido is null)
                {
                    throw new InvalidOperationException("Pedido não encontrado.");
                }

                pedido.MarcarComoLido();

                await _pedidoRepository.UpdateAsync(pedido, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}