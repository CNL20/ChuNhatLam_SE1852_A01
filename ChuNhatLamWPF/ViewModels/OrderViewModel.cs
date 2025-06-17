using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChuNhatLamWPF.Helper.ChuNhatLamWPF.ViewModels;
using ChuNhatLamWPF.Helper;
using System.Windows.Input;
using Lucy_SalesData.DAL.Models;

namespace ChuNhatLamWPF.ViewModels
{ 
    public class OrderViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set { _selectedOrder = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public OrderViewModel()
        {
            Orders = new ObservableCollection<Order>
            {
                new Order { OrderID = 1, OrderDate = "2025-06-01", CustomerName = "Nguyen Van A", TotalAmount = 3000000 },
                new Order { OrderID = 2, OrderDate = "2025-06-10", CustomerName = "Le Thi B", TotalAmount = 1500000 }
            };
            AddCommand = new RelayCommand(_ => AddOrder());
            EditCommand = new RelayCommand(_ => EditOrder(), _ => SelectedOrder != null);
            DeleteCommand = new RelayCommand(_ => DeleteOrder(), _ => SelectedOrder != null);
            RefreshCommand = new RelayCommand(_ => LoadOrders());
        }

        private void LoadOrders() { /* load lại danh sách */ }
        private void AddOrder() { /* thêm mới đơn hàng */ }
        private void EditOrder() { /* sửa đơn hàng */ }
        private void DeleteOrder() { /* xóa đơn hàng */ }
    }
}
