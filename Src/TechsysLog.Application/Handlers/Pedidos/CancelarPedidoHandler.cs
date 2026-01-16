using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Pedidos
{
    /// <summary>
    /// Handler responsável por processar o comando de cancelamento de pedido.
    /// </summary>
    public class CancelarPedidoHandler : ICommandHandler<CancelarPedidoCommand>
    {
        private readonly IPedidoRepository _pedidoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CancelarPedidoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        public CancelarPedidoHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Processa a lógica de cancelamento de um pedido existente com tratamento de exceções.
        /// </summary>
        /// <param name="command">Objeto contendo o identificador do pedido a ser cancelado.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(CancelarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorIdAsync(command.Id, ct);

                if (pedido is null)
                    throw new InvalidOperationException("Pedido não encontrado.");

                pedido.AtualizarStatus(Status.Cancelado);

                await _pedidoRepository.UpdateAsync(pedido, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}