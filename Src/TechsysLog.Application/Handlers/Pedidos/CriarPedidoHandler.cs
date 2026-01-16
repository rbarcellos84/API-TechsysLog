using TechsysLog.Application.Commands.Pedidos;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Pedidos
{
    /// <summary>
    /// Handler responsável por processar o comando de criação de pedido e gerar a notificação inicial.
    /// </summary>
    public class CriarPedidoHandler : ICommandHandler<CriarPedidoCommand>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INotificacaoRepository _notificacaoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CriarPedidoHandler"/>.
        /// </summary>
        /// <param name="pedidoRepository">Instância do repositório de pedidos injetada via DI.</param>
        /// <param name="usuarioRepository">Instância do repositório de usuários injetada via DI.</param>
        /// <param name="notificacaoRepository">Instância do repositório de notificações injetada via DI.</param>
        public CriarPedidoHandler(
            IPedidoRepository pedidoRepository,
            IUsuarioRepository usuarioRepository,
            INotificacaoRepository notificacaoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;
            _notificacaoRepository = notificacaoRepository;
        }

        /// <summary>
        /// Processa a criação de um novo pedido, valida a existência do usuário e gera uma notificação de ingresso.
        /// </summary>
        /// <param name="command">Objeto contendo os dados necessários para a criação do pedido.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(CriarPedidoCommand command, CancellationToken ct)
        {
            try
            {
                var usuario = await _usuarioRepository.ObterPorIdAsync(command.UsuarioId, ct);
                if (usuario is null)
                    throw new InvalidOperationException("Usuário não encontrado.");

                var pedidoExistente = await _pedidoRepository.ObterPorNumeroAsync(command.Numero, ct);
                if (pedidoExistente is not null)
                    throw new InvalidOperationException("Já existe um pedido com este número.");

                var pedido = new Pedido(
                    id: Guid.NewGuid(),
                    usuarioId: command.UsuarioId,
                    numeroPedido: command.Numero,
                    descricao: command.Descricao,
                    itens: command.Itens.ToList(),
                    valorTotal: command.ValorTotal,
                    enderecoEntrega: command.EnderecoEntrega,
                    status: Status.Ingressado,
                    dataCriacao: DateTime.UtcNow
                );

                await _pedidoRepository.AddAsync(pedido, ct);

                var notificacao = new Notificacao(
                    id: Guid.NewGuid(),
                    usuarioId: pedido.UsuarioId,
                    numeroPedido: pedido.NumeroPedido,
                    status: pedido.Status
                );

                await _notificacaoRepository.AddAsync(notificacao, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}