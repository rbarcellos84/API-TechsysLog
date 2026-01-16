using System.Collections.Generic;
using TechsysLog.Application.Dtos.Pedidos;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Pedidos
{
    /// <summary>
    /// Query para obter a lista completa de pedidos registrados no sistema,
    /// independentemente do status de entrega.
    /// Retorna uma coleção de objetos <see cref="PedidoDto"/>.
    /// </summary>
    public class ObterListaPedidosQuery : IQuery<IEnumerable<PedidoDto>>
    {
    }
}
