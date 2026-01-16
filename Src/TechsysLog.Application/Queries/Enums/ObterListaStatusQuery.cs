using TechsysLog.Application.DTOs;
using TechsysLog.Application.Queries.Interfaces;

namespace TechsysLog.Application.Queries.Enums
{
    /// <summary>
    /// Query para obter a lista de valores do enum <see cref="Status"/>.
    /// Retorna uma coleção de objetos <see cref="EnumDto"/> contendo código e descrição.
    /// </summary>
    public class ObterListaStatusQuery : IQuery<IEnumerable<EnumDto>>
    {
    }
}
