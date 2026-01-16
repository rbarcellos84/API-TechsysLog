using TechsysLog.Application.Commands.Entregas;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Entregas
{
    /// <summary>
    /// Handler responsável por registrar uma nova entrega no sistema.
    /// </summary>
    public class RegistrarEntregaHandler : ICommandHandler<RegistrarEntregaCommand>
    {
        private readonly IEntregaRepository _entregaRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RegistrarEntregaHandler"/>.
        /// </summary>
        /// <param name="entregaRepository">Instância do repositório de entregas injetada via DI.</param>
        public RegistrarEntregaHandler(IEntregaRepository entregaRepository)
        {
            _entregaRepository = entregaRepository;
        }

        /// <summary>
        /// Processa a criação e persistência de uma nova entidade de entrega com tratamento de erros.
        /// </summary>
        /// <param name="command">Objeto contendo os dados necessários para o registro da entrega.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(RegistrarEntregaCommand command, CancellationToken ct)
        {
            try
            {
                var entrega = new Entrega(
                    id: Guid.NewGuid(),
                    pedidoId: command.PedidoId,
                    entregadorId: command.EntregadorId,
                    dataPrevista: command.DataPrevista,
                    enderecoDestino: command.EnderecoDestino,
                    status: Status.Ingressado,
                    observacoes: command.Observacoes);

                await _entregaRepository.AddAsync(entrega, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}