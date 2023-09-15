using ArithmeticExpression.Host.Api;

namespace ArithmeticExpression.Host;
public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var allowedCorsOrigins = _configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
        if (allowedCorsOrigins?.Any() ?? false)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(allowedCorsOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }


        services.RegisterApplication();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapCalculatorRoute();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
