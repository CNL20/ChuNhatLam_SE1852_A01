using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ChuNhatLamWPF.Helper;
using ChuNhatLamWPF.Helper.ChuNhatLamWPF.ViewModels;
using Lucy_SalesData.DAL.Models;

namespace ChuNhatLamWPF.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        public ObservableCollection<Customer> Customers { get; set; }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public CustomerViewModel()
        {
            Customers = new ObservableCollection<Customer>
            {
                new Customer { CustomerID = 1, CompanyName = "A", ContactName = "Nguyen Van A", Phone = "0901234567" },
                new Customer { CustomerID = 2, CompanyName = "B", ContactName = "Le Thi B", Phone = "0912345678" }
            };
            AddCommand = new RelayCommand(_ => AddCustomer());
            EditCommand = new RelayCommand(_ => EditCustomer(), _ => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(_ => DeleteCustomer(), _ => SelectedCustomer != null);
            RefreshCommand = new RelayCommand(_ => LoadCustomers());
        }

        private void LoadCustomers()
        {
            // Ví dụ: load lại danh sách từ DB, ở đây chỉ làm mới lại dữ liệu mẫu
            Customers.Clear();
            Customers.Add(new Customer { CustomerID = 1, CompanyName = "A", ContactName = "Nguyen Van A", Phone = "0901234567" });
            Customers.Add(new Customer { CustomerID = 2, CompanyName = "B", ContactName = "Le Thi B", Phone = "0912345678" });
        }

        private void AddCustomer()
        {
            // Đơn giản: thêm khách hàng mới với ID tự tăng
            int newId = Customers.Count > 0 ? Customers[^1].CustomerID + 1 : 1;
            var newCustomer = new Customer
            {
                CustomerID = newId,
                CompanyName = "Tên công ty mới",
                ContactName = "Tên liên hệ mới",
                Phone = "Số điện thoại"
            };
            Customers.Add(newCustomer);
            SelectedCustomer = newCustomer;
            MessageBox.Show("Đã thêm khách hàng mới!");
        }

        private void EditCustomer()
        {
            if (SelectedCustomer != null)
            {
                // Ở đây bạn có thể mở dialog để sửa, ví dụ đơn giản chỉ thông báo
                MessageBox.Show($"Đã sửa khách hàng: {SelectedCustomer.CompanyName}");
                // Nếu có UI nhập liệu, cập nhật lại property của SelectedCustomer ở đây
            }
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                var name = SelectedCustomer.CompanyName;
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
                MessageBox.Show($"Đã xóa khách hàng: {name}");
            }
        }
    }
}