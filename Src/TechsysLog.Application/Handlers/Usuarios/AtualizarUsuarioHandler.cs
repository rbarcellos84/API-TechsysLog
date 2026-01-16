using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Application.Dtos.Usuarios;

namespace TechsysLog.Application.Handlers.Usuarios
{
    /// <summary>
    /// Handler responsável por atualizar dados de um usuário existente.
    /// </summary>
    public class AtualizarUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="AtualizarUsuarioHandler"/>.
        /// </summary>
        /// <param name="usuarioRepository">Instância do repositório de usuários injetada via DI.</param>
        public AtualizarUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Executa a lógica de atualização de um usuário com tratamento de exceções.
        /// </summary>
        /// <param name="dto">Objeto de transferência de dados contendo as novas informações do usuário.</param>
        /// <param name="ct">Token de cancelamento para monitorar solicitações de encerramento da operação.</param>
        /// <returns>Uma <see cref="Task"/> contendo o <see cref="UsuarioDto"/> atualizado ou null caso o usuário não exista.</returns>
        public async Task<UsuarioDto?> HandleAsync(UsuarioDto dto, CancellationToken ct)
        {
            try
            {
                var usuario = await _usuarioRepository.ObterPorIdAsync(dto.Id, ct);

                if (usuario == null)
                    return null;

                usuario.AlterarNome(dto.Nome);
                usuario.AlterarEmail(dto.Email);

                if (!string.IsNullOrWhiteSpace(dto.Senha))
                    usuario.AlterarSenha(HashPassword(dto.Senha));

                await _usuarioRepository.UpdateAsync(usuario, ct);

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

        /// <summary>
        /// Aplica codificação Base64 à senha para persistência.
        /// </summary>
        /// <param name="senha">Senha em formato de texto simples.</param>
        /// <returns>Cadeia de caracteres da senha codificada.</returns>
        private static string HashPassword(string senha)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(senha));
        }
    }
}