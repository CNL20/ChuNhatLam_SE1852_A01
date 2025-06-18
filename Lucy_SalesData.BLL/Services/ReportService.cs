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
    public class ReportService : IReportService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReportService(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IEmployeeRepository employeeRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
        }

        // Tổng doanh thu trong khoảng thời gian
        public decimal GetRevenue(DateTime fromDate, DateTime toDate)
        {
            var orders = _orderRepository.GetOrdersByDateRange(fromDate, toDate);
            var orderIds = orders.Select(o => o.OrderID).ToList();
            
            var details = _orderDetailRepository.GetDetailsByOrderIds(orderIds);
            var revenue = details.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount));
            
            return revenue;
        }

        // Doanh số theo nhân viên
        public IEnumerable<(Employee employee, decimal revenue)> GetRevenueByEmployee(DateTime from, DateTime to)
        {
            var orders = _orderRepository.GetAll()
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .ToList();

            var orderDetails = _orderDetailRepository.GetAll();

            var result = orders
                .GroupBy(o => o.EmployeeID)
                .Select(g =>
                {
                    var employee = _employeeRepository.GetAll().FirstOrDefault(e => e.EmployeeID == g.Key);
                    var employeeOrders = g.Select(o => o.OrderID).ToList();
                    var totalRevenue = orderDetails
                        .Where(od => employeeOrders.Contains(od.OrderID))
                        .Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount));
                    return (employee, totalRevenue);
                });

            return result;
        }

        // Doanh số theo khách hàng
        public IEnumerable<(Customer customer, decimal revenue)> GetRevenueByCustomer(DateTime from, DateTime to)
        {
            var orders = _orderRepository.GetAll()
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .ToList();

            var orderDetails = _orderDetailRepository.GetAll();

            var result = orders
                .GroupBy(o => o.CustomerID)
                .Select(g =>
                {
                    var customer = _customerRepository.GetAll().FirstOrDefault(c => c.CustomerID == g.Key);
                    var customerOrders = g.Select(o => o.OrderID).ToList();
                    var totalRevenue = orderDetails
                        .Where(od => customerOrders.Contains(od.OrderID))
                        .Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount));
                    return (customer, totalRevenue);
                });

            return result;
        }

        // Sản phẩm bán chạy nhất
        public IEnumerable<(Product product, int totalSold)> GetTopSellingProducts(DateTime from, DateTime to, int topN)
        {
            var orders = _orderRepository.GetAll()
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .Select(o => o.OrderID)
                .ToList();

            var orderDetails = _orderDetailRepository.GetAll()
                .Where(od => orders.Contains(od.OrderID))
                .ToList();

            var result = orderDetails
                .GroupBy(od => od.ProductID)
                .Select(g =>
                {
                    var product = _productRepository.GetAll().FirstOrDefault(p => p.ProductID == g.Key);
                    var totalSold = g.Sum(od => od.Quantity);
                    return (product, totalSold);
                })
                .OrderByDescending(x => x.totalSold)
                .Take(topN);

            return result;
        }

        // Báo cáo tồn kho
        public IEnumerable<(Product product, int unitsInStock)> GetInventoryStatus()
        {
            return _productRepository.GetAll()
                .Select(p => (p, p.UnitsInStock));
        }
    }
}
