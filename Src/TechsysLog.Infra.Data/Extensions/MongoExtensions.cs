using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TechsysLog.Infra.Data.Context;
using TechsysLog.Infra.Data.Configurations;

namespace TechsysLog.Infra.Data.Extensions
{
    /// <summary>
    /// Métodos de extensão para configurar o MongoDB no container de injeção de dependência.
    /// Responsável por registrar o contexto e aplicar as configurações de índices.
    /// </summary>
    public static class MongoExtensions
    {
        /// <summary>
        /// Registra o <see cref="MongoDbContext"/> no container de injeção de dependência
        /// e aplica as configurações de índices para todas as coleções.
        /// </summary>
        /// <param name="services">Coleção de serviços da aplicação.</param>
        /// <param name="configuration">Configuração da aplicação contendo a connection string e o nome do banco.</param>
        /// <returns>A coleção de serviços atualizada com o contexto do MongoDB registrado.</returns>
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Obtém a connection string e o nome do banco de dados a partir da configuração
            var connectionString = configuration.GetConnectionString("MongoDb");
            var databaseName = configuration["MongoDbSettings:DatabaseName"];

            /// <summary>
            /// Registra o cliente do MongoDB como singleton.
            /// </summary>
            services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));

            /// <summary>
            /// Registra o contexto do MongoDB como singleton.
            /// </summary>
            services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(connectionString, databaseName));

            // Cria uma instância temporária do contexto para aplicar as configurações de índices
            var context = new MongoDbContext(connectionString, databaseName);

            /// <summary>
            /// Aplica as configurações de índices para cada coleção.
            /// </summary>
            UsuarioConfiguration.Configure(context.Database);
            PedidoConfiguration.Configure(context.Database);
            EntregaConfiguration.Configure(context.Database);
            NotificacaoConfiguration.Configure(context.Database);

            return services;
        }
    }
}
