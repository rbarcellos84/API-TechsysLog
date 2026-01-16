using System;
using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Commands.Usuarios;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Domain.Utils;

namespace TechsysLog.Application.Handlers.Usuarios
{
    /// <summary>
    /// Handler responsável por processar o comando de recuperação de senha.
    /// Gera uma nova credencial, atualiza o repositório e dispara a notificação via e-mail.
    /// </summary>
    public class RecuperarSenhaHandler : ICommandHandlerRetorno<RecuperarSenhaCommand, bool>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RecuperarSenhaHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de usuários injetada via DI.</param>
        /// <param name="emailService">Serviço de mensageria para envio de e-mails.</param>
        public RecuperarSenhaHandler(IUsuarioRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        /// <summary>
        /// Executa a lógica de geração de nova senha e envio de e-mail com tratamento de exceções.
        /// </summary>
        /// <param name="command">Comando contendo o e-mail do usuário que solicita a recuperação.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Uma <see cref="Task"/> contendo true se a operação foi bem-sucedida, ou false caso o usuário não exista.</returns>
        public async Task<bool> HandleAsync(RecuperarSenhaCommand command, CancellationToken ct)
        {
            try
            {
                var usuario = await _repository.ObterPorEmailAsync(command.Email, ct);

                if (usuario is null)
                    return false;

                var novaSenha = Guid.NewGuid().ToString("N")[..8];
                var senhaHash = SenhaHelper.GerarHash(novaSenha);

                usuario.AlterarSenha(senhaHash);
                await _repository.UpdateAsync(usuario, ct);

                await _emailService.EnviarRecuperacaoSenhaAsync(usuario.Email, novaSenha, ct);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}