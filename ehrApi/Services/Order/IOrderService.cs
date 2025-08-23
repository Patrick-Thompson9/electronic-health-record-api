using ehrApi.Models;

namespace ehrApi.Services.Orders;

public interface IOrderService
{
    Task CreateOrder(Order order, bool save = true);
    Task<Order?> GetOrder(Guid id);
    Task<List<Order>> GetAllOrders();
    // TODO: add get order by order number like patient by mrn
    Task<(Order, bool)> UpsertOrder(Order order);
    Task<bool> DeleteOrder(Guid id);
}