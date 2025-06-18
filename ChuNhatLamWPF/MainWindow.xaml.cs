using System.Windows;
using ChuNhatLamWPF.Views;
using Lucy_SalesData.BLL.Interfaces;
using Lucy_SalesData.BLL.Services;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Repositories;
using Lucy_SalesData.DAL.Singleton;
using Microsoft.EntityFrameworkCore;

namespace ChuNhatLamWPF
{
    public partial class MainWindow : Window
    {
        private readonly ICustomerService _customerService;
        private readonly IReportService _reportService;

        public MainWindow()
        {
            InitializeComponent();
            
            // Initialize database context
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-IVLHBMHM;Initial Catalog=Lucy_SalesData;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=True;Trust Server Certificate=True");
            var context = new DataContext(optionsBuilder.Options);

            // Initialize repositories
            var customerRepository = new CustomerRepository(context);
            var orderRepository = new OrderRepository(context);
            var orderDetailRepository = new OrderDetailRepository(context);
            var productRepository = new ProductRepository(context);
            var employeeRepository = new EmployeeRepository(context);

            // Initialize services
            _customerService = new CustomerService(customerRepository);
            _reportService = new ReportService(orderRepository, orderDetailRepository, productRepository, employeeRepository, customerRepository);
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new CustomerView(_customerService);
            win.Show();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductView();
            win.Show();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new OrderView();
            win.Show();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ReportView(_reportService);
            win.Show();
        }
    }
}