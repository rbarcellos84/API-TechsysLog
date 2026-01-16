using AutoMapper;
using TechsysLog.Domain.Entities;
using TechsysLog.Application.Dtos.Entregas;

namespace TechsysLog.Application.Mappings
{
    /// <summary>
    /// Profile de mapeamento para a entidade Entrega.
    /// Responsável apenas por converter de Entrega para EntregaDto.
    /// </summary>
    public class EntregaMapping : Profile
    {
        public EntregaMapping()
        {
            /// <summary>
            /// Mapeia de Entrega para EntregaDto.
            /// </summary>
            CreateMap<Entrega, EntregaDto>();
        }
    }
}
