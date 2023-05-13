using AgendaTenis.WebApi.Bootstrapper;
using AgendaTenis.WebApi.ConfiguracaoDeServicos;

namespace AgendaTenis.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.RegistrarMediator();
        services.RegistrarSwagger();
        services.RegistrarIdentity(Configuration);
        services.RegistrarJogadores(Configuration);
        services.RegistrarPartidas(Configuration);
        services.RegistrarMessageBus(Configuration);
        services.RegistrarAutenticacao(Configuration);
        services.RegistrarRedis(Configuration);
        services.RegistrarBootstrappers();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.RegistrarPolices();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BootstrapperService processadorDeBootstrappers)
    {
        if (env.IsDevelopment())
        {
            processadorDeBootstrappers.Executar();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
