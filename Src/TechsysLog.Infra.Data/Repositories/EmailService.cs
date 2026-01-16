using TechsysLog.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TechsysLog.Infra.Data.Services
{
    public class EmailService : IEmailService
    {
        public Task EnviarRecuperacaoSenhaAsync(string emailDestino, string token, CancellationToken ct)
        {
            /*************************************************************************************** 
            * NOTA ARQUITETURAL:
            * Esta implementação utiliza a saída de Console para simular o envio de mensagens, 
            * priorizando a velocidade de desenvolvimento e eliminando dependências de 
            * infraestrutura externa (SMTP/APIs) nesta fase. 
            * Em um cenário produtivo, este serviço deve ser substituído por um provedor 
            * especializado (ex: SendGrid, AWS SES) para garantir a entregabilidade, 
            * rastreabilidade e segurança das credenciais enviadas.
            **************************************************************************************/

            Console.WriteLine($"Para: {emailDestino}");
            Console.WriteLine($"Assunto: Recuperação de Senha");
            Console.WriteLine($"Mensagem: Sua nova senha é {token}");

            return Task.CompletedTask;
        }
    }
}