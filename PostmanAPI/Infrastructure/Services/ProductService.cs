using Microsoft.EntityFrameworkCore;
using PostmanAPI.Application.DTOs;
using PostmanAPI.Application.Services;
using PostmanAPI.Domain.Entities;
using PostmanAPI.Infrastructure.Data;

namespace PostmanAPI.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponseDto>> GetProductsAsync(string? filter = null)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            var filterParts = filter.Split(':');
            if (filterParts.Length == 2 && filterParts[0] == "name")
            {
                query = query.Where(p => p.Name.Contains(filterParts[1]));
            }
        }

        return await query
            .Select(p => new ProductResponseDto(p.Id, p.Name, p.Price, p.CreatedAt))
            .ToListAsync();
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product == null ? null : new ProductResponseDto(product.Id, product.Name, product.Price, product.CreatedAt);
    }

    public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new ProductResponseDto(product.Id, product.Name, product.Price, product.CreatedAt);
    }

    public async Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        product.Name = updateProductDto.Name;
        product.Price = updateProductDto.Price;

        await _context.SaveChangesAsync();

        return new ProductResponseDto(product.Id, product.Name, product.Price, product.CreatedAt);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
