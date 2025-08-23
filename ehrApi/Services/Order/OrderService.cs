using ehrApi.Data;
using ehrApi.Models;
using ehrApi.Services.Patients;
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
            .OrderByDescending(order => order.OrderNumber)
            .ToListAsync();
    }

    public async Task<(Order, bool)> UpsertOrder(Order order)
    {
        bool wasCreated;

        Order? existingOrder = await _context.Orders
        .Include(order => order.Test)
        .FirstOrDefaultAsync(o => o.Id == order.Id);

        if (existingOrder == null)
        {
            await CreateOrder(order); // save changes is called in this function
            wasCreated = true;
        }
        else
        {
            // Cant edit order number or test with this set up
            existingOrder.OrderType = order.OrderType;
            existingOrder.Notes = order.Notes;
            existingOrder.LastUpdated = DateTime.UtcNow;
            existingOrder.Patient = order.Patient;
            existingOrder.PatientId = order.PatientId;
            await _context.SaveChangesAsync();

            wasCreated = false;
        }
        return (existingOrder ?? order, wasCreated);
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
