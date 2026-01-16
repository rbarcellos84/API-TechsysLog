using Microsoft.AspNetCore.SignalR;

namespace TechsysLog.WebApi.Hubs
{
    /// <summary>
    /// Hub para atualizações em tempo real de pedidos.
    /// </summary>
    public class PedidoHub : Hub
    {
        public async Task EnviarAtualizacaoPedido(string numeroPedido, string status)
        {
            await Clients.All.SendAsync("ReceberAtualizacaoPedido", numeroPedido, status);
        }
    }
}
