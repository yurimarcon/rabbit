using Worker.Service;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Worker.Repository;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("../appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<RabbitMQContext>();

        services.AddScoped<MyService>();

        services.AddScoped<IProductRepository, ProductRepository>();
    })
    .Build();

using (var scope = builder.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // var myService = services.GetRequiredService<MyService>();
    // myService.Run();

    var rabbitMQContext = services.GetRequiredService<RabbitMQContext>();
    rabbitMQContext.SearchInQueue("products");

}
