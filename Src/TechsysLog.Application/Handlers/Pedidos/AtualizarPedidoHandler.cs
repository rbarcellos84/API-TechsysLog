using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Pedidos
{
    /// <summary>
    /// Handler responsável por processar o comando de atualização de pedido.
    /// </summary>
    public class AtualizarPedidoHandler : ICommandHandler<AtualizarPedidoCommand>
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AtualizarPedidoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public AtualizarPedidoHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Processa a lógica de negócio para a atualização de um pedido existente com tratamento de exceções.
        /// </summary>
        /// <param name="command">Objeto contendo os dados identificadores e o novo status do pedido.</param>
        /// <param name="ct">Token de cancelamento para monitorar solicitações de encerramento da operação.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(AtualizarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorIdAsync(command.Id, ct);

                if (pedido is null)
                    throw new InvalidOperationException("Pedido não encontrado.");

                pedido.AtualizarStatus(command.Status);

                await _pedidoRepository.UpdateAsync(pedido, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}