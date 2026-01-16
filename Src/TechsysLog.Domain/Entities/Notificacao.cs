using MongoDB.Bson.Serialization.Attributes;
using TechsysLog.Domain.Entities.Enum;

public class Notificacao
{
    [BsonId]
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public string NumeroPedido { get; private set; }
    public Status Status { get; private set; }
    public DateTime? DataEnvio { get; private set; }
    public bool Lida { get; private set; }
    public DateTime? DataLeitura { get; private set; }

    public Notificacao(Guid id, Guid usuarioId, string numeroPedido, Status status)
    {
        Id = id;
        UsuarioId = usuarioId;
        NumeroPedido = numeroPedido;
        Status = status;
        Lida = false;
        DataLeitura = null;

        if (status == Status.Enviado)
            DataEnvio = DateTime.UtcNow;
    }

    public void MarcarComoLida()
    {
        Lida = true;
        DataLeitura = DateTime.UtcNow;
    }

    public void MarcarComoNaoLida()
    {
        Lida = false;
        DataLeitura = null;
    }

    public void AtualizarStatus(Status novoStatus)
    {
        Status = novoStatus;
        Lida = false;
        DataLeitura = null;

        if (novoStatus == Status.Enviado)
        {
            DataEnvio = DateTime.UtcNow;
        }
    }
}