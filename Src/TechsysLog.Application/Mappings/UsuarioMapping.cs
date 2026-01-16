using AutoMapper;
using TechsysLog.Domain.Entities;
using TechsysLog.Application.Dtos.Usuarios;

namespace TechsysLog.Application.Mappings
{
    /// <summary>
    /// Profile de mapeamento para a entidade Usuario.
    /// Responsável apenas por converter de Usuario para UsuarioDto.
    /// </summary>
    public class UsuarioMapping : Profile
    {
        public UsuarioMapping()
        {
            /// <summary>
            /// Mapeia de Usuario para UsuarioDto.
            /// Não expõe o hash da senha por segurança.
            /// </summary>
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Senha, opt => opt.Ignore());
        }
    }
}
