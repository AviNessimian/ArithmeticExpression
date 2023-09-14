namespace ArithmeticExpression.IntegrationTests.Setup;

public class TestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{

    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        var webHostBuilder = WebHost.CreateDefaultBuilder(Array.Empty<string>());

        var ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        webHostBuilder.ConfigureAppConfiguration(config =>
        {
            var configurationRoot = config.Build();
            config.AddEnvironmentVariables();
        });

        webHostBuilder
            .UseStartup<TStartup>()
            .UseTestServer()
            .ConfigureTestServices(services =>
            {
                services.AddMvc().AddApplicationPart(typeof(TStartup).Assembly);
            });

        return webHostBuilder;
    }
}