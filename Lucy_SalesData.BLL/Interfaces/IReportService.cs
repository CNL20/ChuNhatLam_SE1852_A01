using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.BLL.Interfaces
{
    public interface IReportService
    {
        // Báo cáo tổng doanh thu trong khoảng thời gian
        decimal GetRevenue(DateTime from, DateTime to);

        // Báo cáo doanh số theo từng nhân viên
        IEnumerable<(Employee employee, decimal revenue)> GetRevenueByEmployee(DateTime from, DateTime to);

        // Báo cáo doanh số theo khách hàng
        IEnumerable<(Customer customer, decimal revenue)> GetRevenueByCustomer(DateTime from, DateTime to);

        // Báo cáo sản phẩm bán chạy nhất
        IEnumerable<(Product product, int totalSold)> GetTopSellingProducts(DateTime from, DateTime to, int topN);

        // Báo cáo tồn kho sản phẩm (dựa trên UnitsInStock của Product)
        IEnumerable<(Product product, int unitsInStock)> GetInventoryStatus();
    }
}
