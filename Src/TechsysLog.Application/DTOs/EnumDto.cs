namespace TechsysLog.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) genérico para representar valores de enumeração.
    /// Contém o código numérico e a descrição textual.
    /// </summary>
    public class EnumDto
    {
        /// <summary>
        /// Código numérico associado ao valor do enum.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Descrição textual amigável do valor do enum.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;
    }
}
