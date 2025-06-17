using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.DAL.Interfaces
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetail> GetAll();
        OrderDetail GetById(int orderId, int productId);
        List<OrderDetail> GetByOrderId(int orderId);
        void Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(int orderId, int productId);
        void DeleteByOrderId(int orderId);

    }
}
