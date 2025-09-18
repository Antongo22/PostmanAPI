using PostmanAPI.Application.DTOs;

namespace PostmanAPI.Application.Services;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<List<OrderResponseDto>> GetUserOrdersAsync(int userId);
}
