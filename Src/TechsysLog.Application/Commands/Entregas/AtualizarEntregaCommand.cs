using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.Commands.Entregas;

/// <summary>
/// Comando para atualizar os dados de uma entrega existente.
/// </summary>
public class AtualizarEntregaCommand : ICommand
{
    /// <summary>
    /// Identificador único da entrega.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Data prevista atualizada para a entrega.
    /// </summary>
    public DateTime DataPrevista { get; }

    /// <summary>
    /// Data de conclusão da entrega (opcional).
    /// </summary>
    public DateTime? DataConclusao { get; }

    /// <summary>
    /// Endereço atualizado de destino da entrega.
    /// </summary>
    public string EnderecoDestino { get; }

    /// <summary>
    /// Status atualizado da entrega (ex.: Pendente, Em Transporte, Concluída, Cancelada).
    /// </summary>
    public Status Status { get; }

    /// <summary>
    /// Observações adicionais sobre a entrega.
    /// </summary>
    public string? Observacoes { get; }

    /// <summary>
    /// Construtor do comando de atualização de entrega.
    /// </summary>
    /// <param name="id">Identificador único da entrega.</param>
    /// <param name="dataPrevista">Data prevista atualizada.</param>
    /// <param name="dataConclusao">Data de conclusão (opcional).</param>
    /// <param name="enderecoDestino">Endereço atualizado de destino.</param>
    /// <param name="status">Status atualizado da entrega.</param>
    /// <param name="observacoes">Observações adicionais (opcional).</param>
    public AtualizarEntregaCommand(Guid id, DateTime dataPrevista, DateTime? dataConclusao, string enderecoDestino, Status status, string? observacoes = null)
    {
        Id = id;
        DataPrevista = dataPrevista;
        DataConclusao = dataConclusao;
        EnderecoDestino = enderecoDestino;
        Status = status;
        Observacoes = observacoes;
    }
}
