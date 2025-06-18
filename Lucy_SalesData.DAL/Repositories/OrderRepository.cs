using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Singleton;

namespace Lucy_SalesData.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public List<Order> GetAll() => _context.Orders.ToList();

        public Order GetById(int id) => _context.Orders.FirstOrDefault(o => o.OrderID == id);

        public void Add(Order order)
        {
            if (order.OrderDate == default) order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            var ord = GetById(order.OrderID);
            if (ord != null)
            {
                ord.CustomerID = order.CustomerID;
                ord.EmployeeID = order.EmployeeID;
                ord.OrderDate = order.OrderDate == default ? DateTime.Now : order.OrderDate;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var ord = GetById(id);
            if (ord != null)
            {
                _context.Orders.Remove(ord);
                _context.SaveChanges();
            }
        }

        public List<Order> GetOrdersByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _context.Orders
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                .ToList();
        }
    }
}
