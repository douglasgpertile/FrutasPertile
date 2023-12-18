using FrutasPertile.ProductCatalog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<ProductDiscoverer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/products", async (ProductDiscoverer discoverer) => 
{
    try
    {
        var products = await discoverer.DiscoverProductsAsync();
        return products;
    }
    catch (Exception)
    {
    }

    return Array.Empty<Product>();
});


app.Run();

public class Product
{
    public string? Name { get; set; }
    public string? Price { get; set;} 
    public string? ImageUrl { get; set;}
}