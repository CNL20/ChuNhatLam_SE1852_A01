using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Lucy_SalesData.BLL.Interfaces;
using Lucy_SalesData.DAL.Models;
using ChuNhatLamWPF.Helper;
using System.Windows;

namespace ChuNhatLamWPF.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerService _customerService;
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        private string _companyNameInput = "";
        private string _contactNameInput = "";
        private string _phoneInput = "";
        private string _searchKey;

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
            LoadCustomers();
            InitializeCommands();
        }

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                if (value != null)
                {
                    CompanyNameInput = value.CompanyName;
                    ContactNameInput = value.ContactName;
                    PhoneInput = value.Phone;
                }
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public string CompanyNameInput
        {
            get => _companyNameInput;
            set
            {
                _companyNameInput = value;
                OnPropertyChanged(nameof(CompanyNameInput));
            }
        }

        public string ContactNameInput
        {
            get => _contactNameInput;
            set
            {
                _contactNameInput = value;
                OnPropertyChanged(nameof(ContactNameInput));
            }
        }

        public string PhoneInput
        {
            get => _phoneInput;
            set
            {
                _phoneInput = value;
                OnPropertyChanged(nameof(PhoneInput));
            }
        }

        public string SearchKey
        {
            get => _searchKey;
            set { _searchKey = value; OnPropertyChanged(nameof(SearchKey)); }
        }

        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(ExecuteAdd, CanExecuteAdd);
            EditCommand = new RelayCommand(ExecuteEdit, CanExecuteEdit);
            DeleteCommand = new RelayCommand(ExecuteDelete, CanExecuteDelete);
            RefreshCommand = new RelayCommand(ExecuteRefresh);
            SearchCommand = new RelayCommand(ExecuteSearch);
        }

        private void LoadCustomers()
        {
            var customerList = _customerService.GetAllCustomers();
            Customers = new ObservableCollection<Customer>(customerList);
        }

        private bool CanExecuteAdd(object parameter) => !string.IsNullOrWhiteSpace(CompanyNameInput);

        private void ExecuteAdd(object parameter)
        {
            var result = MessageBox.Show("Bạn có muốn thêm khách hàng mới không?", "Xác nhận thêm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            var customer = new Customer
            {
                CompanyName = CompanyNameInput,
                ContactName = ContactNameInput,
                Phone = PhoneInput
            };
            _customerService.AddCustomer(customer);
            LoadCustomers();
            ClearInputs();
            MessageBox.Show("Thêm thành công");
        }

        private bool CanExecuteEdit(object parameter) => SelectedCustomer != null;

        private void ExecuteEdit(object parameter)
        {
            if (SelectedCustomer != null)
            {
                var result = MessageBox.Show("Bạn có muốn sửa thông tin khách hàng này không?", "Xác nhận sửa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) return;
                SelectedCustomer.CompanyName = CompanyNameInput;
                SelectedCustomer.ContactName = ContactNameInput;
                SelectedCustomer.Phone = PhoneInput;
                _customerService.UpdateCustomer(SelectedCustomer);
                LoadCustomers();
                ClearInputs();
                MessageBox.Show("Edit thành công");
            }
        }

        private bool CanExecuteDelete(object parameter) => SelectedCustomer != null;

        private void ExecuteDelete(object parameter)
        {
            if (SelectedCustomer != null)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes) return;
                _customerService.DeleteCustomer(SelectedCustomer.CustomerID);
                LoadCustomers();
                ClearInputs();
                MessageBox.Show("Xóa thành công");
            }
        }

        private void ExecuteRefresh(object parameter)
        {
            LoadCustomers();
            ClearInputs();
        }

        private void ExecuteSearch(object parameter)
        {
            var customerList = _customerService.Search(SearchKey);
            Customers = new ObservableCollection<Customer>(customerList);
        }

        private void ClearInputs()
        {
            CompanyNameInput = "";
            ContactNameInput = "";
            PhoneInput = "";
            SelectedCustomer = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}