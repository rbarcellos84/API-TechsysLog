using TechsysLog.Application.Commands;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Commands.Entregas
{
    /// <summary>
    /// Comando para registrar uma nova entrega.
    /// </summary>
    public class RegistrarEntregaCommand : ICommand
    {
        /// <summary>
        /// Identificador único do pedido relacionado à entrega.
        /// </summary>
        public Guid PedidoId { get; }

        /// <summary>
        /// Identificador único do entregador responsável pela entrega.
        /// </summary>
        public Guid EntregadorId { get; }

        /// <summary>
        /// Data prevista para a realização da entrega.
        /// </summary>
        public DateTime DataPrevista { get; }

        /// <summary>
        /// Endereço de destino da entrega.
        /// </summary>
        public Endereco EnderecoDestino { get; }

        /// <summary>
        /// Observações adicionais sobre a entrega (opcional).
        /// </summary>
        public string? Observacoes { get; }

        /// <summary>
        /// Construtor do comando de registro de entrega.
        /// </summary>
        /// <param name="pedidoId">Identificador do pedido associado.</param>
        /// <param name="entregadorId">Identificador do entregador responsável.</param>
        /// <param name="dataPrevista">Data prevista para a entrega.</param>
        /// <param name="enderecoDestino">Endereço de destino da entrega.</param>
        /// <param name="observacoes">Observações adicionais (opcional).</param>
        public RegistrarEntregaCommand(Guid pedidoId, Guid entregadorId, DateTime dataPrevista, Endereco enderecoDestino, string? observacoes = null)
        {
            PedidoId = pedidoId;
            EntregadorId = entregadorId;
            DataPrevista = dataPrevista;
            EnderecoDestino = enderecoDestino;
            Observacoes = observacoes;
        }
    }
}
