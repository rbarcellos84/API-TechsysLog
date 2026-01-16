using MongoDB.Driver;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Infra.Data.Configurations
{
    /// <summary>
    /// Configuração da coleção Usuarios no MongoDB.
    /// Responsável por criar índices para otimizar consultas relacionadas aos usuários.
    /// </summary>
    public static class UsuarioConfiguration
    {
        /// <summary>
        /// Aplica as configurações de índices na coleção de usuários.
        /// </summary>
        /// <param name="database">Instância do banco de dados MongoDB.</param>
        public static void Configure(IMongoDatabase database)
        {
            var collection = database.GetCollection<Usuario>("Usuarios");

            /// <summary>
            /// Índice para facilitar consultas por Email.
            /// Garante unicidade do campo Email e permite buscas rápidas.
            /// </summary>
            var emailIndex = Builders<Usuario>.IndexKeys.Ascending(u => u.Email);
            collection.Indexes.CreateOne(
                new CreateIndexModel<Usuario>(
                    emailIndex,
                    new CreateIndexOptions { Unique = true }
                )
            );

            /// <summary>
            /// Índice para facilitar consultas por Nome.
            /// Permite filtrar e ordenar usuários pelo nome.
            /// </summary>
            var nomeIndex = Builders<Usuario>.IndexKeys.Ascending(u => u.Nome);
            collection.Indexes.CreateOne(new CreateIndexModel<Usuario>(nomeIndex));

            /// <summary>
            /// Índice para facilitar consultas por status de Ativo.
            /// Permite filtrar usuários ativos ou desativados.
            /// </summary>
            var ativoIndex = Builders<Usuario>.IndexKeys.Ascending(u => u.Ativo);
            collection.Indexes.CreateOne(new CreateIndexModel<Usuario>(ativoIndex));

            /// <summary>
            /// Índice para facilitar consultas ordenadas pela Data de Criação.
            /// Permite recuperar usuários mais recentes primeiro.
            /// </summary>
            var criadoIndex = Builders<Usuario>.IndexKeys.Descending(u => u.DataCriacao);
            collection.Indexes.CreateOne(new CreateIndexModel<Usuario>(criadoIndex));
        }
    }
}
