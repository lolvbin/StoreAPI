using RealDougAPI.Services;
using RealDougAPI.Contracts;
using FluentValidation;
using FluentValidation.Validators;
using FluentValidation.AspNetCore;
using RealDougAPI.Models;
using RealDougAPI;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// adiciona o fluent validation para que possa registrar todos os validators automaticamente
builder.Services.AddFluentValidationAutoValidation();

// Adiciona o caminho ao validator da criação de produtos
builder.Services.AddValidatorsFromAssemblyContaining<CriarProdutoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AtualizarProdutoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CriarPedidoDTOValidator>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


