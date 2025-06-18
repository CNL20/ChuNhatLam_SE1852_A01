using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.DAL.Models
{
    public class OrderStatistics
    {
        public DateTime Period { get; set; } // Có thể là ngày, tháng hoặc năm
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
