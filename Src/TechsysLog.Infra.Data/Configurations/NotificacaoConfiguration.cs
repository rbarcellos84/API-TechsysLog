using MongoDB.Driver;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Infra.Data.Configurations
{
    /// <summary>
    /// Configuração da coleção Notificacoes no MongoDB.
    /// Responsável por criar índices para otimizar consultas relacionadas às notificações.
    /// </summary>
    public static class NotificacaoConfiguration
    {
        /// <summary>
        /// Aplica as configurações de índices na coleção de notificações.
        /// </summary>
        /// <param name="database">Instância do banco de dados MongoDB.</param>
        public static void Configure(IMongoDatabase database)
        {
            var collection = database.GetCollection<Notificacao>("Notificacoes");

            /// <summary>
            /// Índice para facilitar consultas por UsuarioId.
            /// Permite buscar rapidamente todas as notificações de um usuário específico.
            /// </summary>
            var usuarioIndex = Builders<Notificacao>.IndexKeys.Ascending(n => n.UsuarioId);
            collection.Indexes.CreateOne(new CreateIndexModel<Notificacao>(usuarioIndex));

            /// <summary>
            /// Índice para facilitar consultas por numero do pedido
            /// Permite filtrar notificações por pedido.
            /// </summary>
            var numeroPedido = Builders<Notificacao>.IndexKeys.Ascending(n => n.NumeroPedido);
            collection.Indexes.CreateOne(new CreateIndexModel<Notificacao>(numeroPedido));

            /// <summary>
            /// Índice para facilitar consultas ordenadas pela DataEnvio.
            /// Permite recuperar notificações mais recentes primeiro.
            /// </summary>
            var dataIndex = Builders<Notificacao>.IndexKeys.Descending(n => n.DataEnvio);
            collection.Indexes.CreateOne(new CreateIndexModel<Notificacao>(dataIndex));

            /// <summary>
            /// Índice para facilitar consultas por status de leitura (Lida).
            /// Permite filtrar notificações já lidas ou não lidas.
            /// </summary>
            var lidaIndex = Builders<Notificacao>.IndexKeys.Ascending(n => n.Lida);
            collection.Indexes.CreateOne(new CreateIndexModel<Notificacao>(lidaIndex));
        }
    }
}
