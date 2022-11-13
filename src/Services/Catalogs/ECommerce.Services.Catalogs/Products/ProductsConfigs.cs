using BuildingBlocks.Abstractions.CQRS.Events;
using BuildingBlocks.Abstractions.Persistence;
using BuildingBlocks.Abstractions.Web.Module;
using ECommerce.Services.Catalogs.Products.Data;
using ECommerce.Services.Catalogs.Products.Features.CreatingProduct;
using ECommerce.Services.Catalogs.Products.Features.DebitingProductStock;
using ECommerce.Services.Catalogs.Products.Features.GettingProductById;
using ECommerce.Services.Catalogs.Products.Features.GettingProductsView;
using ECommerce.Services.Catalogs.Products.Features.ReplenishingProductStock;
using ECommerce.Services.Catalogs.Products.Features.UpdatingProduct;
using ECommerce.Services.Catalogs.Shared;

namespace ECommerce.Services.Catalogs.Products;

internal class ProductsConfigs : IModuleConfiguration
{
    public const string Tag = "Product";
    public const string ProductsPrefixUri = $"{SharedModulesConfiguration.CatalogModulePrefixUri}/products";

    public WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDataSeeder, ProductDataSeeder>();
        builder.Services.AddSingleton<IEventMapper, ProductEventMapper>();

        return builder;
    }

    public Task<WebApplication> ConfigureModule(WebApplication app)
    {
        return Task.FromResult<WebApplication>(app);
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapCreateProductsEndpoint()
            .MapUpdateProductEndpoint()
            .MapDebitProductStockEndpoint()
            .MapReplenishProductStockEndpoint()
            .MapGetProductByIdEndpoint()
            .MapGetProductsViewEndpoint();
    }
}
