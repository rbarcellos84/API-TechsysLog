using MongoDB.Bson.Serialization.Attributes;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Domain.Entities
{
    public class Entrega
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid EntregadorId { get; private set; }
        public DateTime DataPrevista { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public Endereco EnderecoDestino { get; private set; }
        public Status Status { get; private set; }
        public string? Observacoes { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        public Entrega(Guid id, Guid pedidoId, Guid entregadorId, DateTime dataPrevista, Endereco enderecoDestino, Status status, string? observacoes)
        {
            Id = id;
            PedidoId = pedidoId;
            EntregadorId = entregadorId;
            DataPrevista = dataPrevista;
            EnderecoDestino = enderecoDestino;
            Status = status;
            Observacoes = observacoes;
            DataRegistro = DateTime.UtcNow;
        }

        public void AtualizarStatus(Status status) { Status = status; DataAtualizacao = DateTime.UtcNow; }
        public void AtualizarDataConclusao(DateTime dataConclusao) { DataConclusao = dataConclusao; DataAtualizacao = DateTime.UtcNow; }
        public void AtualizarObservacoes(string observacoes) { Observacoes = observacoes; DataAtualizacao = DateTime.UtcNow; }
    }
}
