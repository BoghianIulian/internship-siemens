using System.Collections.Generic;
using SieMarketProject.models;

namespace SieMarketProject.Data;

public class OrderRepository
{
    private List<Customer> _customers = new List<Customer>();
    private List<Order> _orders = new List<Order>();
    private List<OrderItem> _orderItems = new List<OrderItem>(); 

    public void AddCustomer(Customer customer) => _customers.Add(customer);
    public void AddOrder(Order order) => _orders.Add(order);

    public void AddOrderItem(OrderItem item) => _orderItems.Add(item);
    
    public List<Customer> GetAllCustomers() => _customers;
    public List<Order> GetAllOrders() => _orders;
    public List<OrderItem> GetAllItems() => _orderItems;
}