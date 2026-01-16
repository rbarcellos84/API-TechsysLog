using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechsysLog.Application.Commands.Pedidos
{
    /// <summary>
    /// Comando responsável por transportar os dados necessários para a atualização do status de um pedido.
    /// </summary>
    public class AtualizarStatusPedidoCommand
    {
        /// <summary>
        /// Obtém ou define o número identificador único do pedido (ex: "123457").
        /// </summary>
        /// <example>123457</example>
        public string NumeroPedido { get; set; }

        /// <summary>
        /// Obtém ou define o novo status para o qual o pedido deve transicionar.
        /// </summary>
        /// <remarks>
        /// Os status comuns são: 1 (Ingressado), 2 (Em Processamento), 3 (Enviado), 4 (Entregue).
        /// </remarks>
        public int NovoStatus { get; set; }
    }
}