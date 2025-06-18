using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.DAL.Interfaces
{
    public interface IReportRepository
    {
        List<OrderStatistics> GetOrderStatisticsByMonth(DateTime from, DateTime to);
    }
}
