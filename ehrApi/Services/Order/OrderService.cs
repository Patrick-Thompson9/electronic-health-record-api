using ehrApi.Models;

namespace ehrApi.Services.Orders;

public class OrderService : IOrderService
{
    private static readonly Dictionary<Guid, Order> _orders = new();

    public void CreateOrder(Order order)
    {
        _orders.Add(order.Id, order);
    }

    public Order GetOrder(Guid id)
    {
        return _orders[id];
    }

    public Order UpsertOrder(Order order)
    {
        _orders[order.Id] = order;
        return order;
    }

    public void DeleteOrder(Guid id)
    {
        _orders.Remove(id);
    }
}
