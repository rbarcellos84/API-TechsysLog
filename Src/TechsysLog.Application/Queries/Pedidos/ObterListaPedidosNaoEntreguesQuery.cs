using System.Collections.Generic;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Pedidos
{
    /// <summary>
    /// Query para obter a lista de pedidos que ainda não foram entregues.
    /// Retorna uma coleção de objetos <see cref="PedidoDto"/>.
    /// </summary>
    public class ObterListaPedidosNaoEntreguesQuery : IQuery<IEnumerable<PedidoDto>>
    {
    }
}
