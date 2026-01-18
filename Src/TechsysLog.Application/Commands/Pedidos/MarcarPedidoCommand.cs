using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechsysLog.Application.Commands.Pedidos
{
    /// <summary>
    /// Marcar o pedido como lido
    /// </summary>
    public class MarcarPedidoCommand : ICommand
    {
        public string NumeroPedido { get; set; }
        public MarcarPedidoCommand(string numeroPedido)
        {
            NumeroPedido = numeroPedido;
        }
    }
}
