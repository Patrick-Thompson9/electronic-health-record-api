using ehrApi.Data;
using ehrApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Orders;

public class OrderService : IOrderService
{
    private readonly EhrApiContext _context;

    public OrderService(EhrApiContext context)
    {
        _context = context;
    }

    public async Task CreateOrder(Order order, bool save = true)
    {
        _context.Orders.Add(order);

        if (save) await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetOrder(Guid id)
    {
        return await _context.Orders
        .Include(order => order.Test)
        .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _context.Orders
            .Include(order => order.Test)
            .ToListAsync();
    }

    public async Task<Order> UpsertOrder(Order order)
    {
        var existingOrder = await _context.Orders.FindAsync(order.Id);
        if (existingOrder == null)
        {
            await CreateOrder(order); // save changes is called in this function
        }
        else
        {
            existingOrder.OrderType = order.OrderType;
            existingOrder.Notes = order.Notes;
            existingOrder.LastUpdated = DateTime.UtcNow;
            existingOrder.Patient = order.Patient;
            existingOrder.PatientId = order.PatientId;
            existingOrder.Test = order.Test;
            await _context.SaveChangesAsync();
        }
        return existingOrder ?? order;
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
