using System.Collections.Generic;
using System.Linq;
using SieMarketProject.models;

namespace SieMarketProject.Services;

public class MarketService
{
    // 2.3 Find customer with highest total spend across all their orders
    public string GetTopSpender(List<Order> orders, List<Customer> customers)
    {
        var topSpendingData = orders
            .GroupBy(o => o.CustomerId)
            .Select(g => new { 
                CustomerId = g.Key, 
                TotalAmount = g.Sum(o => o.GetFinalPrice()) 
            })
            .OrderByDescending(x => x.TotalAmount)
            .FirstOrDefault();

        if (topSpendingData == null) return "No data available";

        var customer = customers.FirstOrDefault(c => c.Id == topSpendingData.CustomerId);
        return $"{customer?.Name ?? "Unknown"} - Total spent: {topSpendingData.TotalAmount:F2}€";
    }

    // 2.4 Bonus: Popular products by total quantity sold
    public List<KeyValuePair<string, int>> GetProductPopularityDescending(List<Order> orders)
    {
        return orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.ProductName)
            .Select(g => new KeyValuePair<string, int>(g.Key, g.Sum(i => i.Quantity)))
            .OrderByDescending(x => x.Value) 
            .ToList();
    }
}