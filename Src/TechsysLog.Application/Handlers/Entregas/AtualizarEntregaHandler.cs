using TechsysLog.Application.Commands.Entregas;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Handlers.Entregas
{
    /// <summary>
    /// Handler responsável por processar o comando de atualização de entrega.
    /// </summary>
    public class AtualizarEntregaHandler : ICommandHandler<AtualizarEntregaCommand>
    {
        private readonly IEntregaRepository _entregaRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AtualizarEntregaHandler"/>.
        /// </summary>
        /// <param name="entregaRepository">Instância do repositório de entregas injetada via DI.</param>
        public AtualizarEntregaHandler(IEntregaRepository entregaRepository)
        {
            _entregaRepository = entregaRepository;
        }

        /// <summary>
        /// Processa a lógica de negócio para a atualização de uma entrega existente com tratamento de exceções.
        /// </summary>
        /// <param name="command">Objeto contendo os dados identificadores e campos a serem atualizados na entrega.</param>
        /// <param name="ct">Token de cancelamento para monitorar solicitações de encerramento da operação.</param>
        /// <returns>Uma <see cref="Task"/> que representa a conclusão da operação assíncrona.</returns>
        public async Task HandleAsync(AtualizarEntregaCommand command, CancellationToken ct)
        {
            try
            {
                var entrega = await _entregaRepository.ObterPorIdAsync(command.Id, ct);

                if (entrega is null)
                    throw new InvalidOperationException("Entrega não encontrada.");

                entrega.AtualizarStatus(command.Status);

                if (command.DataConclusao.HasValue)
                    entrega.AtualizarDataConclusao(command.DataConclusao.Value);

                if (!string.IsNullOrWhiteSpace(command.Observacoes))
                    entrega.AtualizarObservacoes(command.Observacoes);

                await _entregaRepository.UpdateAsync(entrega, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}