using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Pedidos
{
    /// <summary>
    /// Handler responsável por atualizar o status de um pedido e sincronizar a notificação correspondente.
    /// </summary>
    public class AtualizarStatusPedidoHandler
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly INotificacaoRepository _notificacaoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AtualizarStatusPedidoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        /// <param name="notificacaoRepository">Instância do repositório de notificações injetada via DI.</param>
        public AtualizarStatusPedidoHandler(
            IPedidoRepository pedidoRepository,
            INotificacaoRepository notificacaoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _notificacaoRepository = notificacaoRepository;
        }

        /// <summary>
        /// Executa a atualização do status do pedido e atualiza a notificação vinculada, se existente.
        /// </summary>
        /// <param name="command">Objeto contendo o número do pedido e o novo status.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(AtualizarStatusPedidoCommand command, CancellationToken ct)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPorNumeroAsync(command.NumeroPedido, ct);
                if (pedido == null)
                    throw new Exception("Pedido não encontrado.");

                var statusEnum = (Status)command.NovoStatus;

                pedido.AtualizarStatus(statusEnum);
                await _pedidoRepository.UpdateAsync(pedido, ct);

                var notificacoes = await _notificacaoRepository.ListarPorUsuarioAsync(pedido.UsuarioId, ct);
                var notificacao = notificacoes.FirstOrDefault(n => n.NumeroPedido == pedido.NumeroPedido);

                if (notificacao != null)
                {
                    notificacao.AtualizarStatus(statusEnum);
                    await _notificacaoRepository.UpdateAsync(notificacao, ct);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}