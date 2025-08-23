using ehrApi.Models;

namespace ehrApi.Services.Orders;

public interface IOrderService
{
    Task CreateOrder(Order order, bool save = true);
    Task<Order?> GetOrder(Guid id);
    Task<List<Order>> GetAllOrders();
    Task<Order?> GetOrderByOrderNumber(string OrderNumber);
    Task<(Order, bool)> UpsertOrder(Order order);
    Task<bool> DeleteOrder(Guid id);
}