using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.DAL.Singleton
{
    public class DataContext
    {
        private static DataContext _instance;
        public static DataContext Instance => _instance ??= new DataContext();

        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Employee> Employees { get; set; }

        private DataContext()
        {
            Customers = new List<Customer>();
            Orders = new List<Order>();
            OrderDetails = new List<OrderDetail>();
            Categories = new List<Category>();
            Products = new List<Product>();
            Employees = new List<Employee>();
        }
    }
}
