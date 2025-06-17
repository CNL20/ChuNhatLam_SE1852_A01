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
        private readonly List<Order> _orders;       
        public OrderRepository()
        {
            _orders = DataContext.Instance.Orders;
        }
        public List<Order> GetAll() => _orders;
        public Order GetById(int id) => _orders.FirstOrDefault(o => o.OrderID == id);
        public void Add(Order order)
        {
            order.OrderID = _orders.Count > 0 ? _orders.Max(o => o.OrderID) + 1 : 1;
            if (order.OrderDate == default) order.OrderDate = DateTime.Now;
            _orders.Add(order);
        }
        public void Update(Order order)
        {
            order.OrderDate = DateTime.Now;
            var ord = GetById(order.OrderID);
            if (ord != null)
            {
                ord.CustomerID = order.CustomerID;
                ord.EmployeeID = order.EmployeeID;
                ord.OrderDate = order.OrderDate == default ? DateTime.Now : order.OrderDate;
            }
        }
        public void Delete(int id)
        {
            var ord = GetById(id);
            if (ord != null)
            {
                _orders.Remove(ord);
            }
        }
        
    }
}
