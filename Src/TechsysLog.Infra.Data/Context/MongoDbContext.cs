using MongoDB.Driver;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Infra.Data.Context
{
    /// <summary>
    /// Contexto principal do MongoDB.
    /// Responsável por fornecer acesso às coleções do banco de dados.
    /// </summary>
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Inicializa a conexão com o MongoDB.
        /// </summary>
        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        /// <summary>
        /// Exposição direta do IMongoDatabase para configurações de índices.
        /// </summary>
        public IMongoDatabase Database => _database;

        /// <summary>
        /// Coleção de usuários.
        /// </summary>
        public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("Usuarios");

        /// <summary>
        /// Coleção de pedidos.
        /// </summary>
        public IMongoCollection<Pedido> Pedidos => _database.GetCollection<Pedido>("Pedidos");

        /// <summary>
        /// Coleção de entregas.
        /// </summary>
        public IMongoCollection<Entrega> Entregas => _database.GetCollection<Entrega>("Entregas");

        /// <summary>
        /// Coleção de notificações.
        /// </summary>
        public IMongoCollection<Notificacao> Notificacoes => _database.GetCollection<Notificacao>("Notificacoes");
    }
}
