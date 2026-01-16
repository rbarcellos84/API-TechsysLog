using System;

namespace TechsysLog.Domain.Exceptions
{
    /// <summary>
    /// Exceção de domínio utilizada para representar violações de regras de negócio.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Cria uma nova instância de DomainException com uma mensagem de erro.
        /// </summary>
        /// <param name="message">Mensagem descrevendo a violação da regra de negócio.</param>
        public DomainException(string message) : base(message)
        {
        }

        /// <summary>
        /// Cria uma nova instância de DomainException com uma mensagem de erro e uma exceção interna.
        /// </summary>
        /// <param name="message">Mensagem descrevendo a violação da regra de negócio.</param>
        /// <param name="innerException">Exceção interna que originou o erro.</param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
