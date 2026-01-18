using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Interfaces;
using TechsysLog.Domain.Entities.Enum;

namespace TechsysLog.Application.Queries.Pedidos
{
    /// <summary>
    /// Query para obter uma lista de pedidos filtrada por um estado específico no sistema.
    /// Útil para carregar visões segmentadas do Dashboard (ex: apenas pedidos "Enviados").
    /// Retorna uma coleção de objetos <see cref="PedidoDto"/>.
    /// </summary>
    public class ObterListaPedidosPorEstadoQuery : IQuery<IEnumerable<PedidoDto>>
    {
        public Status Status { get; set; }

        public ObterListaPedidosPorEstadoQuery(Status status)
        {
            Status = status;
        }
    }
}
