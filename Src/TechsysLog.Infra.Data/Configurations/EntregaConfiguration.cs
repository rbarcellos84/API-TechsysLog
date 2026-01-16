using MongoDB.Driver;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Infra.Data.Configurations
{
    /// <summary>
    /// Configuração da coleção Entregas no MongoDB.
    /// Responsável por criar índices para otimizar consultas relacionadas às entregas.
    /// </summary>
    public static class EntregaConfiguration
    {
        /// <summary>
        /// Aplica as configurações de índices na coleção de entregas.
        /// </summary>
        /// <param name="database">Instância do banco de dados MongoDB.</param>
        public static void Configure(IMongoDatabase database)
        {
            var collection = database.GetCollection<Entrega>("Entregas");

            /// <summary>
            /// Índice para facilitar consultas por PedidoId.
            /// Permite buscar rapidamente todas as entregas associadas a um pedido específico.
            /// </summary>
            var pedidoIndex = Builders<Entrega>.IndexKeys.Ascending(e => e.PedidoId);
            collection.Indexes.CreateOne(new CreateIndexModel<Entrega>(pedidoIndex));

            /// <summary>
            /// Índice para facilitar consultas por DataPrevista.
            /// Permite ordenar e filtrar entregas pela data prevista de conclusão.
            /// </summary>
            var dataIndex = Builders<Entrega>.IndexKeys.Ascending(e => e.DataPrevista);
            collection.Indexes.CreateOne(new CreateIndexModel<Entrega>(dataIndex));
        }
    }
}
