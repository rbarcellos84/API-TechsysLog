using Microsoft.AspNetCore.SignalR;

public class NotificationAccessHub : Hub
{
    /// <summary>
    /// Hub para atualizações em tempo real de notificações.
    /// </summary>
    public async Task EnviarNotificacao(Guid usuarioId, string mensagem)
    {
        await Clients.User(usuarioId.ToString()).SendAsync("ReceberNotificacao", mensagem);
    }
}
