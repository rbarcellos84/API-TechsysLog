using AutoMapper;
using TechsysLog.Domain.Entities;
using TechsysLog.Application.Dtos.Pedidos;

namespace TechsysLog.Application.Mappings
{
    /// <summary>
    /// Profile de mapeamento para a entidade Pedido.
    /// Responsável apenas por converter de Pedido para PedidoDto.
    /// </summary>
    public class PedidoMapping : Profile
    {
        public PedidoMapping()
        {
            /// <summary>
            /// Mapeia de Pedido para PedidoDto.
            /// </summary>
            CreateMap<Pedido, PedidoDto>();
        }
    }
}
