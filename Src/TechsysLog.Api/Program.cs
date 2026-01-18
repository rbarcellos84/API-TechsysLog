using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using TechsysLog.Infra.Data.Extensions;
using TechsysLog.IoC;
using TechsysLog.Web.Api.Security;
using TechsysLog.WebApi.Hubs;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TechsysLog.Web.Api.Security;

// ============================================================
// CONFIGURAÇÃO GLOBAL DO MONGODB (ADICIONE ISSO AQUI)
// ============================================================
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. CONFIGURAÇÃO DE AUTENTICAÇÃO JWT
// ============================================================

// Lendo a chave DIRETAMENTE do appsettings.json para bater com o gerador
var secretKeyFromConfig = builder.Configuration["JwtSettings:Secret"];
var keyBytes = Encoding.ASCII.GetBytes(secretKeyFromConfig);

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearer =>
{
    bearer.RequireHttpsMetadata = false;
    bearer.SaveToken = true;
    bearer.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes), // Usa a chave do JSON
        ValidateIssuer = false,
        ValidateAudience = false
    };

    // Configuração para o SignalR (access_token via URL)
    bearer.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// ============================================================
// 2. SWAGGER COM CONFIGURAÇÃO DE SEGURANÇA (CADEADO)
// ============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechsysLog API", Version = "v1" });

    // Define o esquema de segurança JWT para o Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticação JWT usando o esquema Bearer. Exemplo: 'Bearer {seu_token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// ============================================================
// 3. INJEÇÃO DE DEPENDÊNCIAS E SERVIÇOS
// ============================================================
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddApplicationDependencies(); // Suas dependências de IoC
builder.Services.AddMongoDbContext(builder.Configuration);

// Configuração de CORS: Permite que o front-end acesse a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ============================================================
// 4. CONSTRUÇÃO E MIDDLEWARES (ORDEM IMPORTANTE!)
// ============================================================
var app = builder.Build();

// Swagger sempre no topo para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// O CORS deve vir ANTES da Autenticação
app.UseCors("DefaultPolicy");

app.UseAuthentication(); // Identifica quem é o usuário
app.UseAuthorization();  // Verifica o que o usuário pode fazer

// Mapeamento de Rotas e Hubs
app.MapControllers();
app.MapHub<PedidoHub>("/hubs/pedidos");

app.Run();