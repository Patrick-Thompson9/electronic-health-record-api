using ehrApi.Models;

namespace ehrApi.Services.Orders;

public interface IOrderService
{
    void CreateOrder(Order order);
    Order GetOrder(Guid id);
    Order UpsertOrder(Order order);
    void DeleteOrder(Guid id);
}