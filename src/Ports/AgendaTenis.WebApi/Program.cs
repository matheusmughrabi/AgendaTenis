using AgendaTenis.Core.Identity.AcessoDados;
using AgendaTenis.Core.Identity.AcessoDados.Repositorios;
using AgendaTenis.Core.Identity.Dominio;
using AgendaTenis.Core.Identity.Token;
using AgendaTenis.Core.Identity.Utils;
using AgendaTenis.Core.Jogadores.AcessoDados;
using AgendaTenis.WebApi.ContainerDI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// Container de injeção de dependência
var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrarMediator();
builder.Services.AddScoped<IIdentityRepositorio, IdentityRepositorio>();
builder.Services.AddScoped<ISenhaHasher<UsuarioEntity>, SenhaHasher<UsuarioEntity>>();
builder.Services.AddScoped<IPasswordHasher<UsuarioEntity>, PasswordHasher<UsuarioEntity>>();

var section = builder.Configuration.GetSection(nameof(JwtOptions));
var chave = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(section["ChaveSecreta"]));

builder.Services.AddScoped(c => new JwtOptions()
{
    ExpiracaoEmSegundos = int.Parse(section["ExpiracaoEmSegundos"]),
    SigningCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256)
});
builder.Services.AddScoped<GeradorDeToken>();

builder.Services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

builder.Services.AddDbContext<JogadoresDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Jogadores")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.RegistrarAutenticacao(builder.Configuration);

var app = builder.Build();

// Pipeline dos requests
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
