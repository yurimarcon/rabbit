using Worker.Service;
using Microsoft.EntityFrameworkCore;
using Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worker.Repository;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        // Adicionar o contexto do banco de dados
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

        // Adicionar serviço RabbitMQContext
        services.AddTransient<RabbitMQContext>();

        // Adicionar seu serviço customizado
        services.AddTransient<MyService>();

        services.AddScoped<IProductRepository, ProductRepository>();
    })
    .Build();

using (var scope = builder.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var myService = services.GetRequiredService<MyService>();
    myService.Run();

    var rabbitMQContext = services.GetRequiredService<RabbitMQContext>();
    rabbitMQContext.SearchInQueue("products");

}




// new RabbitMQContext().SearchInQueue("products");
