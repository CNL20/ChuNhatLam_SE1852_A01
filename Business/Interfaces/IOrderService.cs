using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.BLL.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(int id);
        void Add(Order order, List<OrderDetail> orderDetails);
        void Update(Order order);
        void Delete(int id);
        (Order order, List<OrderDetail> details) GetOrderWithDetails(int orderId);
    }
}
