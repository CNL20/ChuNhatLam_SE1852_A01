using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Lucy_SalesData.DAL.Repositories;
using Lucy_SalesData.DAL.Singleton;
using ChuNhatLamWPF.ViewModels;

namespace ChuNhatLamWPF.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        public OrderView()
        {
            InitializeComponent();
            var context = DbContextFactory.Create();
            var orderRepository = new OrderRepository(context);
            var customerRepository = new CustomerRepository(context);
            var employeeRepository = new EmployeeRepository(context);
            var orderDetailRepository = new OrderDetailRepository(context);
            DataContext = new OrderViewModel(orderRepository, customerRepository, employeeRepository, orderDetailRepository);
        }
    }
}
