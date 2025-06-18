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
        private readonly DataContext _context;

        public OrderDetailRepository(DataContext context)
        {
            _context = context;
        }

        public List<OrderDetail> GetAll() => _context.OrderDetails.ToList();

        public List<OrderDetail> GetByOrderId(int orderId)
            => _context.OrderDetails.Where(od => od.OrderID == orderId).ToList();

        public OrderDetail GetById(int orderId, int productId)
            => _context.OrderDetails.FirstOrDefault(od => od.OrderID == orderId && od.ProductID == productId);

        public void Add(OrderDetail orderDetail)
        {
            if (GetById(orderDetail.OrderID, orderDetail.ProductID) != null)
                throw new Exception("Chi tiết sản phẩm này đã tồn tại trong đơn hàng!");
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void Update(OrderDetail orderDetail)
        {
            var od = GetById(orderDetail.OrderID, orderDetail.ProductID);
            if (od != null)
            {
                od.UnitPrice = orderDetail.UnitPrice;
                od.Quantity = orderDetail.Quantity;
                od.Discount = orderDetail.Discount;
                _context.SaveChanges();
            }
        }

        public void Delete(int orderId, int productId)
        {
            var od = GetById(orderId, productId);
            if (od != null)
            {
                _context.OrderDetails.Remove(od);
                _context.SaveChanges();
            }
        }

        public void DeleteByOrderId(int orderId)
        {
            var list = _context.OrderDetails.Where(od => od.OrderID == orderId).ToList();
            if (list.Count > 0)
            {
                _context.OrderDetails.RemoveRange(list);
                _context.SaveChanges();
            }
        }

        public List<OrderDetail> GetDetailsByOrderIds(List<int> orderIds)
        {
            return _context.OrderDetails
                .Where(od => orderIds.Contains((int)od.OrderID))
                .ToList();
        }
    }
}