// MyService.cs
using Infrastructure;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

public class MyService
{
    private readonly DatabaseContext _context;
    private readonly ILogger<MyService> _logger;

    public MyService(DatabaseContext context, ILogger<MyService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Run()
    {
        _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);

        // Exemplo de uso do contexto do banco de dados
        if (!_context.Products.Any())
        {
            _context.Products.Add(new Product { Name = "Product3", Value = 10.0 });
            _context.SaveChanges();
        }

        var products = _context.Products.ToList();
        foreach (var product in products)
        {
            _logger.LogInformation("Product: {name}, Value: {value}", product.Name, product.Value);
        }
    }
}