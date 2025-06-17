using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.BLL.Interfaces;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Repositories;

namespace Lucy_SalesData.BLL.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public List<OrderDetail> GetAll() => _orderDetailRepository.GetAll();

        public List<OrderDetail> GetByOrderId(int orderId) => _orderDetailRepository.GetByOrderId(orderId);

        public OrderDetail GetById(int orderId, int productId) => _orderDetailRepository.GetById(orderId, productId);

        public void Add(OrderDetail orderDetail) => _orderDetailRepository.Add(orderDetail);

        public void Update(OrderDetail orderDetail) => _orderDetailRepository.Update(orderDetail);

        public void Delete(int orderId, int productId) => _orderDetailRepository.Delete(orderId, productId);

        public void DeleteByOrderId(int orderId) => _orderDetailRepository.DeleteByOrderId(orderId);
    }
}
