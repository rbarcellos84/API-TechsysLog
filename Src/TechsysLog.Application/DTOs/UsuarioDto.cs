namespace TechsysLog.Application.Dtos.Usuarios
{
    /// <summary>
    /// Data Transfer Object para usuário.
    /// Usado para entrada/saída na API.
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário (texto puro, será transformada em hash).
        /// </summary>
        public string Senha { get; set; } = string.Empty;

        /// <summary>
        /// Indica se o usuário está ativo.
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Data de criação do usuário.
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Data de atualização do usuário (se houver).
        /// </summary>
        public DateTime? DataAtualizacao { get; set; }
    }
}
