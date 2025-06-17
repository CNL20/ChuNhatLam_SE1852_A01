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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public List<Order> GetAll() => _orderRepository.GetAll();

        public Order GetById(int id) => _orderRepository.GetById(id);

        public void Add(Order order, List<OrderDetail> orderDetails)
        {
            _orderRepository.Add(order);
            foreach (var detail in orderDetails)
            {
                detail.OrderID = order.OrderID;
                _orderDetailRepository.Add(detail);
            }
        }

        public void Update(Order order) => _orderRepository.Update(order);

        public void Delete(int id)
        {
            _orderDetailRepository.DeleteByOrderId(id);
            _orderRepository.Delete(id);
        }

        public (Order order, List<OrderDetail> details) GetOrderWithDetails(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null) return (null, null);

            var details = _orderDetailRepository.GetByOrderId(orderId);
            return (order, details);
        }

    }
}
