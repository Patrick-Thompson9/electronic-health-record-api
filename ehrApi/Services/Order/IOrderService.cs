using ehrApi.Models;

namespace ehrApi.Services.Orders;

public interface IOrderService
{
    Task CreateOrder(Order order);
    Task<Order?> GetOrder(Guid id);
    Task<List<Order>> GetAllOrders();
    Task<Order> UpsertOrder(Order order);
    Task<bool> DeleteOrder(Guid id);
}