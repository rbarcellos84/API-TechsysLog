using System.Collections.Generic;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Pedidos
{
    /// <summary>
    /// Query para obter a lista de pedidos que já foram entregues.
    /// Retorna uma coleção de objetos <see cref="PedidoDto"/>.
    /// </summary>
    public class ObterListaPedidosEntreguesQuery : IQuery<IEnumerable<PedidoDto>>
    {
    }
}
