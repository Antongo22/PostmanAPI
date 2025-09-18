using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostmanAPI.Application.DTOs;
using PostmanAPI.Application.Services;

namespace PostmanAPI.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var products = app.MapGroup("/products").WithTags("Products").RequireAuthorization();

        products.MapGet("/", async (IProductService productService, [FromQuery] string? filter) =>
        {
            var result = await productService.GetProductsAsync(filter);
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .WithSummary("Get products with optional filtering")
        .Produces<List<ProductResponseDto>>(200);

        products.MapGet("/{id:int}", async (int id, IProductService productService) =>
        {
            var product = await productService.GetProductByIdAsync(id);
            return product == null 
                ? Results.NotFound(new ErrorResponseDto("Product not found", 404))
                : Results.Ok(product);
        })
        .WithName("GetProductById")
        .WithSummary("Get product by ID")
        .Produces<ProductResponseDto>(200)
        .Produces<ErrorResponseDto>(404);

        products.MapPost("/", async ([FromBody] CreateProductDto createProductDto, IProductService productService) =>
        {
            try
            {
                var product = await productService.CreateProductAsync(createProductDto);
                return Results.Created($"/products/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new ErrorResponseDto(ex.Message, 400));
            }
        })
        .WithName("CreateProduct")
        .WithSummary("Create new product")
        .Produces<ProductResponseDto>(201)
        .Produces<ErrorResponseDto>(400);

        products.MapPut("/{id:int}", async (int id, [FromBody] UpdateProductDto updateProductDto, IProductService productService) =>
        {
            var product = await productService.UpdateProductAsync(id, updateProductDto);
            return product == null 
                ? Results.NotFound(new ErrorResponseDto("Product not found", 404))
                : Results.Ok(product);
        })
        .WithName("UpdateProduct")
        .WithSummary("Update product")
        .Produces<ProductResponseDto>(200)
        .Produces<ErrorResponseDto>(404);

        products.MapDelete("/{id:int}", async (int id, IProductService productService) =>
        {
            var result = await productService.DeleteProductAsync(id);
            return result 
                ? Results.NoContent()
                : Results.NotFound(new ErrorResponseDto("Product not found", 404));
        })
        .WithName("DeleteProduct")
        .WithSummary("Delete product")
        .Produces(204)
        .Produces<ErrorResponseDto>(404);
    }
}
