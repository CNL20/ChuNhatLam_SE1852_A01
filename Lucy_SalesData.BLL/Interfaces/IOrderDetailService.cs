using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.BLL.Interfaces
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetAll();
        List<OrderDetail> GetByOrderId(int orderId);
        OrderDetail GetById(int orderId, int productId);
        void Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        void Delete(int orderId, int productId);
        void DeleteByOrderId(int orderId);

    }
}
