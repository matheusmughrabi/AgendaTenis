using AgendaTenis.WebApi.ContainerDI;

// Container de injeção de dependência
var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistrarMediator();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline dos requests
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
