namespace TechsysLog.Domain.Utils
{
    /// <summary>
    /// Classe utilitária para manipulação de enums.
    /// Fornece métodos para converter valores de enumeração em listas contendo código e descrição,
    /// facilitando o uso em combos/dropdowns no frontend.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gera uma lista de valores de um enum genérico, retornando código e descrição.
        /// </summary>
        /// <typeparam name="T">Tipo do enum a ser convertido.</typeparam>
        /// <returns>
        /// Uma coleção de tuplas contendo:
        /// - <c>Codigo</c>: valor numérico do enum.
        /// - <c>Descricao</c>: nome textual do enum.
        /// </returns>
        public static IEnumerable<(int Codigo, string Descricao)> ListarEnum<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => (
                           Codigo: Convert.ToInt32(e),
                           Descricao: e.ToString()
                       ));
        }
    }
}
