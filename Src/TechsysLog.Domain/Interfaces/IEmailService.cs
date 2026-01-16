using System.Threading;
using System.Threading.Tasks;

namespace TechsysLog.Domain.Interfaces
{
    /// <summary>
    /// Define o contrato para serviços de envio de e-mails.
    /// Responsável por operações relacionadas à comunicação por e-mail,
    /// como envio de mensagens de recuperação de senha.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Envia um e-mail de recuperação de senha para o usuário informado.
        /// </summary>
        /// <param name="emailDestino">Endereço de e-mail do destinatário.</param>
        /// <param name="token">Token gerado para redefinição de senha.</param>
        /// <param name="ct">Token de cancelamento da operação assíncrona.</param>
        Task EnviarRecuperacaoSenhaAsync(string emailDestino, string token, CancellationToken ct);
    }
}
