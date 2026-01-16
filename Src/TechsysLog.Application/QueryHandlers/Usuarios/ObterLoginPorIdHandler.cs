using System;
using System.Threading;
using System.Threading.Tasks;
using TechsysLog.Application.Dtos.Usuarios;
using TechsysLog.Application.Queries.Usuarios;
using TechsysLog.Application.QueryHandlers.Interfaces;
using TechsysLog.Domain.Interfaces;

namespace TechsysLog.Application.QueryHandlers.Usuarios
{
    /// <summary>
    /// Handler responsável por processar a query de recuperação de dados de um usuário pelo seu identificador único.
    /// </summary>
    public class ObterLoginPorIdHandler : IQueryHandler<ObterLoginPorIdQuery, UsuarioDto>
    {
        private readonly IUsuarioRepository _repository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ObterLoginPorIdHandler"/>.
        /// </summary>
        /// <param name="repository">Instância do repositório de usuários injetada via DI.</param>
        public ObterLoginPorIdHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa a busca do usuário pelo ID e realiza o mapeamento para o DTO de retorno com tratamento de exceções.
        /// </summary>
        /// <param name="query">Objeto de consulta contendo o ID do usuário.</param>
        /// <param name="ct">Token de cancelamento para operações assíncronas.</param>
        /// <returns>Um objeto <see cref="UsuarioDto"/> com os detalhes do usuário localizado.</returns>
        /// <exception cref="InvalidOperationException">Lançada caso o usuário não seja encontrado na base de dados.</exception>
        public async Task<UsuarioDto> HandleAsync(ObterLoginPorIdQuery query, CancellationToken ct)
        {
            try
            {
                var usuario = await _repository.ObterPorIdAsync(query.Id, ct);

                if (usuario is null)
                    throw new InvalidOperationException("Usuário não encontrado.");

                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Ativo = usuario.Ativo,
                    DataCriacao = usuario.DataCriacao,
                    DataAtualizacao = usuario.DataAtualizacao
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}