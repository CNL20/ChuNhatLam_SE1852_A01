using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Interfaces;
using ChuNhatLamWPF.Helper;

namespace ChuNhatLamWPF.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public ObservableCollection<OrderViewItem> Orders { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        private OrderViewItem _selectedOrder;
        public OrderViewItem SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
                if (_selectedOrder != null)
                {
                    var order = _orderRepository.GetById(_selectedOrder.OrderID);
                    SelectedCustomer = Customers.FirstOrDefault(c => c.CustomerID == order.CustomerID);
                    SelectedEmployee = Employees.FirstOrDefault(e => e.EmployeeID == order.EmployeeID);
                    OrderDateInput = order.OrderDate;
                }
            }
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set { _selectedEmployee = value; OnPropertyChanged(); }
        }

        private DateTime _orderDateInput = DateTime.Now;
        public DateTime OrderDateInput
        {
            get => _orderDateInput;
            set { _orderDateInput = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public OrderViewModel(IOrderRepository orderRepository, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _orderDetailRepository = orderDetailRepository;

            Orders = new ObservableCollection<OrderViewItem>();
            Customers = new ObservableCollection<Customer>(_customerRepository.GetAll());
            Employees = new ObservableCollection<Employee>(_employeeRepository.GetAll());

            LoadOrders();

            AddCommand = new RelayCommand(_ => AddOrder());
            EditCommand = new RelayCommand(_ => EditOrder(), _ => SelectedOrder != null);
            DeleteCommand = new RelayCommand(_ => DeleteOrder(), _ => SelectedOrder != null);
            RefreshCommand = new RelayCommand(_ => LoadOrders());
        }

        private void LoadOrders()
        {
            Orders.Clear();
            var orders = _orderRepository.GetAll();
            var customers = Customers.ToDictionary(c => c.CustomerID, c => c.CompanyName);
            foreach (var order in orders)
            {
                var details = _orderDetailRepository.GetByOrderId(order.OrderID);
                decimal total = details.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount));
                Orders.Add(new OrderViewItem
                {
                    OrderID = order.OrderID,
                    OrderDate = order.OrderDate,
                    CustomerName = customers.ContainsKey(order.CustomerID) ? customers[order.CustomerID] : "",
                    TotalAmount = total
                });
            }
        }

        private void AddOrder()
        {
            if (SelectedCustomer == null || SelectedEmployee == null) return;
            var newOrder = new Order
            {
                CustomerID = SelectedCustomer.CustomerID,
                EmployeeID = SelectedEmployee.EmployeeID,
                OrderDate = OrderDateInput
            };
            _orderRepository.Add(newOrder);
            LoadOrders();
        }

        private void EditOrder()
        {
            if (SelectedOrder != null && SelectedCustomer != null && SelectedEmployee != null)
            {
                var order = _orderRepository.GetById(SelectedOrder.OrderID);
                order.CustomerID = SelectedCustomer.CustomerID;
                order.EmployeeID = SelectedEmployee.EmployeeID;
                order.OrderDate = OrderDateInput;
                _orderRepository.Update(order);
                LoadOrders();
            }
        }

        private void DeleteOrder()
        {
            if (SelectedOrder != null)
            {
                _orderRepository.Delete(SelectedOrder.OrderID);
                LoadOrders();
            }
        }
    }

    public class OrderViewItem : ViewModelBase
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}