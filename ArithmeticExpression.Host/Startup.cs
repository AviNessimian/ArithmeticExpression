using ArithmeticExpression.Host.Api;

namespace ArithmeticExpression.Host;
public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly string[] _allowedCorsOrigins;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _allowedCorsOrigins = _configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        if (_allowedCorsOrigins?.Any() ?? false)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(_allowedCorsOrigins)
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
        if (_allowedCorsOrigins?.Any() ?? false)
        {
            app.UseCors("CorsPolicy");
        }
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapCalculatorRoute();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
