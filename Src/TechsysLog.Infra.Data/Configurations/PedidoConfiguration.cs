using MongoDB.Driver;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Infra.Data.Configurations
{
    /// <summary>
    /// Configuração da coleção Pedidos no MongoDB.
    /// Responsável por criar índices para otimizar consultas relacionadas aos pedidos.
    /// </summary>
    public static class PedidoConfiguration
    {
        /// <summary>
        /// Aplica as configurações de índices na coleção de pedidos.
        /// </summary>
        /// <param name="database">Instância do banco de dados MongoDB.</param>
        public static void Configure(IMongoDatabase database)
        {
            var collection = database.GetCollection<Pedido>("Pedidos");

            /// <summary>
            /// Índice para facilitar consultas por Número do Pedido.
            /// Garante unicidade do campo NumeroPedido e permite buscas rápidas.
            /// </summary>
            var numeroIndex = Builders<Pedido>.IndexKeys.Ascending(p => p.NumeroPedido);
            collection.Indexes.CreateOne(
                new CreateIndexModel<Pedido>(
                    numeroIndex,
                    new CreateIndexOptions { Unique = true }
                )
            );
        }
    }
}
