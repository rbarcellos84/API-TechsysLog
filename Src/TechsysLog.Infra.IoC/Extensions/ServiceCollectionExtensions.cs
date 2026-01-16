using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechsysLog.Infra.Data.Extensions;

namespace TechsysLog.IoC
{
    /// <summary>
    /// Métodos de extensão para configuração de serviços adicionais da aplicação.
    /// Autenticação JWT, MongoDB e SignalR.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configura autenticação JWT para proteger as rotas da API.
        /// </summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        /// <summary>
        /// Configura o MongoDB e aplica as configurações de índices.
        /// </summary>
        public static IServiceCollection AddMongoConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDbContext(configuration);
            return services;
        }

        /// <summary>
        /// Configura o SignalR para notificações em tempo real.
        /// </summary>
        public static IServiceCollection AddSignalRConfiguration(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }
    }
}
