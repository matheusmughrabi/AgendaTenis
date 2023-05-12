using AgendaTenis.WebApi.ConfiguracaoDeServicos;

// Container de injeção de dependência
var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrarMediator();
builder.Services.RegistrarSwagger();
builder.Services.RegistrarIdentity(builder.Configuration);
builder.Services.RegistrarJogadores(builder.Configuration);
builder.Services.RegistrarPartidas(builder.Configuration);
builder.Services.RegistrarMessageBus(builder.Configuration);
builder.Services.RegistrarAutenticacao(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.RegistrarPolices();

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
