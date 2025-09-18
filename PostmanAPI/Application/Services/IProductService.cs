using PostmanAPI.Application.DTOs;

namespace PostmanAPI.Application.Services;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetProductsAsync(string? filter = null);
    Task<ProductResponseDto?> GetProductByIdAsync(int id);
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
    Task<bool> DeleteProductAsync(int id);
}
