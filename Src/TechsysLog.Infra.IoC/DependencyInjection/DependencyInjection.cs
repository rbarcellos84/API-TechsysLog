using Microsoft.Extensions.DependencyInjection;
using TechsysLog.Application.Handlers;
using TechsysLog.Application.Handlers.Pedidos;
using TechsysLog.Application.Handlers.Usuarios;
using TechsysLog.Application.Queries;
using TechsysLog.Application.Queries.Pedidos;
using TechsysLog.Application.QueryHandlers;
using TechsysLog.Application.QueryHandlers.Enums;
using TechsysLog.Application.QueryHandlers.Pedidos;
using TechsysLog.Application.QueryHandlers.Usuarios;
using TechsysLog.Domain.Interfaces;
using TechsysLog.Infra.Data.Repositories;
using TechsysLog.Infra.Data.Services;

namespace TechsysLog.IoC
{
    /// <summary>
    /// Classe utilitária para configuração da Injeção de Dependência do projeto.
    /// Define o tempo de vida (Scope) dos componentes da camada de aplicação e infraestrutura.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registra as dependências necessárias para o funcionamento da aplicação no container do ASP.NET Core.
        /// </summary>
        /// <param name="services">Coleção de serviços do sistema.</param>
        /// <returns>A coleção de serviços configurada.</returns>
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // --- REPOSITÓRIOS E SERVIÇOS DE INFRAESTRUTURA ---
            /// <summary>
            /// Registro dos repositórios de dados e serviços externos.
            /// </summary>
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IEmailService, EmailService>();

            // --- HANDLERS - ESCRITA (COMMANDS) ---
            /// <summary>
            /// Registro dos Handlers responsáveis pelo processamento de comandos que alteram o estado do sistema (CQRS - Write Side).
            /// </summary>
            services.AddScoped<EfetuarLoginHandler>();
            services.AddScoped<CriarUsuarioHandler>();
            services.AddScoped<RecuperarSenhaHandler>();
            services.AddScoped<AtualizarUsuarioHandler>();

            services.AddScoped<CriarPedidoHandler>();
            services.AddScoped<AtualizarPedidoHandler>();
            services.AddScoped<AtualizarStatusPedidoHandler>();
            services.AddScoped<CancelarPedidoHandler>();
            services.AddScoped<MarcarPedidoHandler>();

            // --- QUERY HANDLERS - LEITURA (QUERIES) ---
            /// <summary>
            /// Registro dos Handlers responsáveis por consultas e retorno de dados (CQRS - Read Side).
            /// </summary>
            services.AddScoped<ObterListaStatusHandler>();

            services.AddScoped<ObterListaPedidosHandler>();
            services.AddScoped<ObterListaPedidosPorEstadoHandler>();
            services.AddScoped<ObterPedidoPorNumeroHandler>();
            services.AddScoped<ObterPedidoPorIdHandler>();

            services.AddScoped<ObterLoginPorEmailHandler>();
            services.AddScoped<ObterLoginPorIdHandler>();

            return services;
        }
    }
}