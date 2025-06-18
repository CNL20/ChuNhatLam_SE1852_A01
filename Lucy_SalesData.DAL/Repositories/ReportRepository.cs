using System;
using System.Collections.Generic;
using System.Linq;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Singleton;
using Lucy_SalesData.DAL.Interfaces;

namespace Lucy_SalesData.DAL.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }

        public List<OrderStatistics> GetOrderStatisticsByMonth(DateTime from, DateTime to)
        {
            var orders = _context.Orders
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .ToList();
            var details = _context.OrderDetails;

            var result = orders
                .GroupBy(o => new DateTime(o.OrderDate.Year, o.OrderDate.Month, 1))
                .Select(g =>
                {
                    var monthOrders = g.ToList();
                    var monthOrderIds = monthOrders.Select(o => o.OrderID).ToList();
                    var totalRevenue = details
                        .Where(od => monthOrderIds.Contains(od.OrderID))
                        .Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount));
                    return new OrderStatistics
                    {
                        Period = g.Key,
                        OrderCount = monthOrders.Count,
                        TotalRevenue = totalRevenue
                    };
                })
                .OrderByDescending(x => x.Period)
                .ToList();
            return result;
        }
    }
}