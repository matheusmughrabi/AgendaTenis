namespace AgendaTenis.WebApi.ConfiguracaoDeServicos;

public static class Redis
{
    public static void RegistrarRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "agendaTenis";
        });
    }
}
