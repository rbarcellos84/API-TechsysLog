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

// ============================================================
// CONFIGURAÇÃO GLOBAL DO MONGODB (ADICIONE ISSO AQUI)
// ============================================================
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. CONFIGURAÇÃO DE AUTENTICAÇÃO JWT
// ============================================================
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecurity.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };

    // Permite que o SignalR receba o token via QueryString (essencial para WebSockets)
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
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
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
app.MapHub<NotificationAccessHub>("/hubs/notificacoes");

app.Run();