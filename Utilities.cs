namespace SearchConversation;

public static class Utilities
{
    /// <summary>
    /// Helper extension method to allow all CORS requests.
    /// </summary>
    public static IServiceCollection AllowAllCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });
        return services;
    }
}