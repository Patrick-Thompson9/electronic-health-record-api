using ehrApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ehrApi.Services.Generators;

public class OrderNumberGenerator : IOrderNumberGenerator
{
    private readonly EhrApiContext _context;

    public OrderNumberGenerator(EhrApiContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateOrderNumber()
    {
        var latestOrder = await _context.Orders
            .OrderByDescending(order => order.OrderNumber)
            .FirstOrDefaultAsync();

        if (latestOrder == null)
        {
            return "000000001";
        }

        var latestOrderNum = int.Parse(latestOrder.OrderNumber);
        int newOrderNum = latestOrderNum + 1;

        return newOrderNum.ToString("D10");
    }
}