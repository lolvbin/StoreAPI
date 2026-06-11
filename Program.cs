using StoreAPI.Services;
using StoreAPI.Contracts;
using FluentValidation;
using FluentValidation.Validators;
using FluentValidation.AspNetCore;
using StoreAPI.Models;
using StoreAPI;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// adiciona o fluent validation para que possa registrar todos os validators automaticamente
builder.Services.AddFluentValidationAutoValidation();

// Adiciona o caminho ao validator da criação de produtos
builder.Services.AddValidatorsFromAssemblyContaining<CriarProdutoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AtualizarProdutoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CriarPedidoDTOValidator>();

// Adiciona o caminho ao validator da criação de usuários
builder.Services.AddScoped<IAuthService, AuthService>();

// 1. Configuração do JWT Bearer Authentication
var secretKey = builder.Configuration["JwtSettings:Secret"]!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Adiciona o contexto do banco de dados e configura para usar o SQLite, pegando a string de conexão do appsettings.json.j
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adicionado para aceitar escrever o status do pedido por extenso e converter para Enum
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });


builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


