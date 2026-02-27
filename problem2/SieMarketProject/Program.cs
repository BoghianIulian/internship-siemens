using System;
using System.Collections.Generic;
using SieMarketProject.models;
using SieMarketProject.Data;
using SieMarketProject.Services;

var repo = new OrderRepository();
var service = new MarketService();

// 1. Setup Customers
var c1 = new Customer { Id = 1, Name = "Alice" };
var c2 = new Customer { Id = 2, Name = "Bob" };
repo.AddCustomer(c1);
repo.AddCustomer(c2);

// 2. Setup Order Items
var item1 = new OrderItem { Id = 10, ProductName = "Smartphone", Quantity = 1, UnitPrice = 600 };
var item2 = new OrderItem { Id = 11, ProductName = "Cables", Quantity = 5, UnitPrice = 10 };

// 3. Create Orders
var order1 = new Order { Id = 100, CustomerId = 1, Items = new List<OrderItem> { item1 } };
var order2 = new Order { Id = 101, CustomerId = 2, Items = new List<OrderItem> { item2 } };

// 4. Save to Repository
repo.AddOrder(order1);
repo.AddOrder(order2);
repo.AddOrderItem(item1);
repo.AddOrderItem(item2);

// 5. Run Analytics
var topSpender = service.GetTopSpender(repo.GetAllOrders(), repo.GetAllCustomers());
var sortedPopularity = service.GetProductPopularityDescending(repo.GetAllOrders());

Console.WriteLine($"Top Spender: {topSpender}");
Console.WriteLine("Popularity Ranking:");
foreach(var p in sortedPopularity)
{
    Console.WriteLine($"- {p.Key}: {p.Value} units sold");
}