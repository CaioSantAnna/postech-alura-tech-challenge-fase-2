using GerenciadorAcervo.API.Configuracoes;
using GerenciadorAcervo.API.Filtros;
using GerenciadorAcervo.Dados;
using GerenciadorAcervo.Dados.Interfaces;
using GerenciadorAcervo.Modelos.Funcoes;
using GerenciadorAcervo.Servicos;
using GerenciadorAcervo.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ExceptionFilterCustomizado());
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(string), 500));
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(string), 400));
    options.Filters.Add(new ProducesAttribute("application/json"));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var dadosConfiguracao = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUsuariosService, UsuariosService>();
builder.Services.AddTransient<ICategoriasService, CategoriasService>();
builder.Services.AddTransient<IAtributosService, AtributosService>();
builder.Services.AddTransient<ISubCategoriasService, SubCategoriasService>();
builder.Services.AddTransient<IItensAcervoService, ItensAcervoService>();
builder.Services.AddTransient<IAutenticacoesService, AutenticacoesService>();
builder.Services.AddTransient<IContatosService, ContatosService>();
builder.Services.AddScoped<RetornoApi, RetornoApi>();
builder.Services.AdicionarJwtConfiguracao(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
                        "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite seu token no campo abaixo.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

    var caminhoArquivoDocumentacaoInformacoes = Path.Combine(System.AppContext.BaseDirectory, "GerenciadorAcervo.API.xml");
    options.IncludeXmlComments(caminhoArquivoDocumentacaoInformacoes);
});
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(dadosConfiguracao).CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
