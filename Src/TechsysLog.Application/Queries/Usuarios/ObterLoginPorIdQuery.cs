using System;
using TechsysLog.Application.Dtos.Usuarios;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Usuarios
{
    /// <summary>
    /// Query para obter os dados de login de um usuário específico
    /// a partir do seu identificador único (ID).
    /// Retorna um objeto <see cref="UsuarioDto"/> correspondente.
    /// </summary>
    public class ObterLoginPorIdQuery : IQuery<UsuarioDto>
    {
        /// <summary>
        /// Identificador único do usuário que será consultado.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Construtor da query para obter login por ID.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        public ObterLoginPorIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
