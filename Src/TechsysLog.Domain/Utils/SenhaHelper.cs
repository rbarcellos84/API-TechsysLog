using System.Security.Cryptography;
using System.Text;

namespace TechsysLog.Domain.Utils
{
    /// <summary>
    /// Classe utilitária para operações relacionadas a senhas.
    /// Fornece métodos para geração de hash e validação de senhas.
    /// </summary>
    public static class SenhaHelper
    {
        /// <summary>
        /// Gera um hash SHA256 para a senha informada.
        /// </summary>
        /// <param name="senha">Senha em texto puro que será convertida em hash.</param>
        /// <returns>Uma string Base64 representando o hash da senha.</returns>
        public static string GerarHash(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Aplica codificação Base64 à senha para persistência inicial.
        /// </summary>
        /// <param name="senha">Senha em formato de texto simples.</param>
        /// <returns>Cadeia de caracteres da senha codificada.</returns>
        public static string HashPassword(string senha)
        {
            if (string.IsNullOrEmpty(senha)) return string.Empty;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(senha));
        }

        /// <summary>
        /// Valida se a senha informada corresponde ao hash armazenado.
        /// </summary>
        /// <param name="senhaInformada">Senha em texto puro informada pelo usuário.</param>
        /// <param name="senhaHashArmazenada">Hash da senha armazenada no sistema.</param>
        /// <returns><c>true</c> se a senha informada corresponde ao hash armazenado; caso contrário, <c>false</c>.</returns>
        public static bool ValidarSenha(string senhaInformada, string senhaHashArmazenada)
        {
            var hashInformada = HashPassword(senhaInformada);
            return hashInformada == senhaHashArmazenada;
        }
    }
}
