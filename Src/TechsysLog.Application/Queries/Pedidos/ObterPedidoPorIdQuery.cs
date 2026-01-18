using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Pedidos
{
    public class ObterPedidoPorIdQuery : IQuery<PedidoDto>
    {
        /// <summary>
        /// Número único do pedido que será consultado.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Construtor da query para obter pedido por número.
        /// </summary>
        /// <param name="numeroPedido">Número do pedido.</param>
        public ObterPedidoPorIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
