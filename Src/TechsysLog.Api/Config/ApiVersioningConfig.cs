using Asp.Versioning;

namespace TechsysLog.Web.Api.Config;

/// <summary>
/// Classe de configuração para habilitar versionamento da API.
/// Utiliza o pacote Microsoft.AspNetCore.Mvc.Versioning.
/// </summary>
public static class ApiVersioningConfig
{
    public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new UrlSegmentApiVersionReader()
            );
        }).AddApiExplorer(options => 
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
