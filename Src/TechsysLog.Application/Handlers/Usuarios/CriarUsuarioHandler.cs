using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Application.Dtos.Usuarios;
using TechsysLog.Application.Commands.Usuarios;
using TechsysLog.Application.Handlers;
using TechsysLog.Domain.Utils;

namespace TechsysLog.Application.Handlers.Usuarios
{
    /// <summary>
    /// Handler responsável por criar um novo usuário implementando o contrato de retorno.
    /// </summary>
    public class CriarUsuarioHandler : ICommandHandlerRetorno<CriarUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CriarUsuarioHandler"/>.
        /// </summary>
        /// <param name="usuarioRepository">Instância do repositório de usuários injetada via DI.</param>
        public CriarUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Executa o processo de criação de um novo usuário com tratamento de exceções.
        /// </summary>
        /// <param name="command">Objeto contendo os dados para o novo cadastro.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> contendo o <see cref="UsuarioDto"/> do usuário recém-criado.</returns>
        public async Task<UsuarioDto> HandleAsync(CriarUsuarioCommand command, CancellationToken ct)
        {
            try
            {
                var pass = SenhaHelper.HashPassword(command.Senha);

                var usuario = new Usuario(
                    id: Guid.NewGuid(),
                    nome: command.Nome,
                    email: command.Email,
                    senhaHash: pass 
                );

                await _usuarioRepository.AddAsync(usuario, ct);

                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Ativo = usuario.Ativo,
                    DataCriacao = usuario.DataCriacao,
                    DataAtualizacao = usuario.DataAtualizacao
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}