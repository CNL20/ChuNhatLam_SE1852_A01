using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Singleton;

namespace Lucy_SalesData.DAL.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly List<OrderDetail> _orderDetails;

        public OrderDetailRepository()
        {
            _orderDetails = DataContext.Instance.OrderDetails;
        }

        public List<OrderDetail> GetAll() => _orderDetails;

        public List<OrderDetail> GetByOrderId(int orderId)
            => _orderDetails.Where(od => od.OrderID == orderId).ToList();

        public OrderDetail GetById(int orderId, int productId)
            => _orderDetails.FirstOrDefault(od => od.OrderID == orderId && od.ProductID == productId);

        public void Add(OrderDetail orderDetail)
        {
            
            if (GetById(orderDetail.OrderID, orderDetail.ProductID) != null)
                throw new Exception("Chi tiết sản phẩm này đã tồn tại trong đơn hàng!");
            _orderDetails.Add(orderDetail);
        }

        public void Update(OrderDetail orderDetail)
        {
            var od = GetById(orderDetail.OrderID, orderDetail.ProductID);
            if (od != null)
            {
                od.UnitPrice = orderDetail.UnitPrice;
                od.Quantity = orderDetail.Quantity;
                od.Discount = orderDetail.Discount;
            }
        }

        public void Delete(int orderId, int productId)
        {
            var od = GetById(orderId, productId);
            if (od != null) _orderDetails.Remove(od);
        }

        public void DeleteByOrderId(int orderId)
        {
            _orderDetails.RemoveAll(od => od.OrderID == orderId);
        }
    }
}