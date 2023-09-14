
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using ArithmeticExpression.Host;

namespace ArithmeticExpression.Host;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateWebHostBuilder(args);
        IWebHost webhost = host.Build();
        webhost.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        var webHostBuilder = WebHost.CreateDefaultBuilder(args);
        webHostBuilder.UseStartup<Startup>();
        return webHostBuilder;
    }
}
