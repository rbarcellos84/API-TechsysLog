using MongoDB.Bson.Serialization.Attributes;
using TechsysLog.Domain.Entities.Enum;
using TechsysLog.Domain.Exceptions;

namespace TechsysLog.Domain.Entities
{
    public class Pedido
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string NumeroPedido { get; private set; }
        public string Descricao { get; private set; }
        public List<string> Itens { get; private set; }
        public decimal ValorTotal { get; private set; }
        public Endereco EnderecoEntrega { get; private set; }
        public Status Status { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataEnvio { get; private set; }
        public bool Lida { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        public Pedido(
            Guid id,
            Guid usuarioId,
            string numeroPedido,
            string descricao,
            List<string> itens,
            decimal valorTotal,
            Endereco enderecoEntrega,
            Status status,
            DateTime dataCriacao,
            bool lida)
        {
            Id = id;
            UsuarioId = usuarioId;
            NumeroPedido = numeroPedido;
            Descricao = descricao;
            Itens = itens;
            ValorTotal = valorTotal;
            EnderecoEntrega = enderecoEntrega;
            Status = status;
            DataCriacao = dataCriacao;
            Lida = lida;
        }

        public void AtualizarStatus(Status novoStatus)
        {
            if (Status == Status.Cancelado) 
                throw new DomainException("Pedido cancelado não pode ser atualizado.");
            
            if (novoStatus < Status) 
                throw new DomainException("Não é permitido retroceder o status do pedido.");

            if (novoStatus == Status.Enviado)
                DataEnvio = DateTime.UtcNow;

            Status = novoStatus; 
            DataAtualizacao = DateTime.UtcNow;
            Lida = false;
        }

        public void MarcarComoLido()
        {
            Lida = true;
        }
    }
}
