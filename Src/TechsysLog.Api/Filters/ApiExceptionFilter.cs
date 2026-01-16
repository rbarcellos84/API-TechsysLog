using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechsysLog.Domain.Exceptions;

namespace TechsysLog.Web.Api.Filters;

/// <summary>
/// Filtro global de exceções para padronizar as respostas de erro da API.
/// Captura exceções lançadas durante a execução dos controllers e retorna objetos JSON consistentes.
/// </summary>
public class ApiExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Método executado automaticamente quando ocorre uma exceção não tratada no fluxo da requisição.
    /// Realiza a triagem do erro e define o status HTTP correspondente.
    /// </summary>
    /// <param name="context">Contexto da exceção contendo detalhes do erro e da requisição.</param>
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;

        /// <summary>
        /// Tratamento para erros de domínio (regras de negócio e validações).
        /// Retorna HTTP 400 (Bad Request).
        /// </summary>
        if (ex is DomainException)
        {
            context.Result = new ObjectResult(new
            {
                error = ex.Message,
                type = "DomainException"
            })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            context.ExceptionHandled = true;
            return;
        }

        /// <summary>
        /// Tratamento para recursos não encontrados no banco de dados.
        /// Retorna HTTP 404 (Not Found).
        /// </summary>
        if (ex is KeyNotFoundException)
        {
            context.Result = new ObjectResult(new
            {
                error = ex.Message,
                type = "NotFound"
            })
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            context.ExceptionHandled = true;
            return;
        }

        /// <summary>
        /// Tratamento genérico para erros inesperados ou falhas de infraestrutura.
        /// Retorna HTTP 500 (Internal Server Error).
        /// </summary>
        context.Result = new ObjectResult(new
        {
            error = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
            type = "ServerError"
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }
}