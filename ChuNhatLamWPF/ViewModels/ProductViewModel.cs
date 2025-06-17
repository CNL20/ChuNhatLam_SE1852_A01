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
   
    public class ProductViewModel : ViewModelBase
    {
        public ObservableCollection<Product> Products { get; set; }
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public ProductViewModel()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { ProductID = 1, ProductName = "Iphone 15", UnitPrice = 25000000, Quantity = 10 },
                new Product { ProductID = 2, ProductName = "Samsung S24", UnitPrice = 22000000, Quantity = 15 }
            };
            AddCommand = new RelayCommand(_ => AddProduct());
            EditCommand = new RelayCommand(_ => EditProduct(), _ => SelectedProduct != null);
            DeleteCommand = new RelayCommand(_ => DeleteProduct(), _ => SelectedProduct != null);
            RefreshCommand = new RelayCommand(_ => LoadProducts());
        }

        private void LoadProducts() { /* load lại danh sách */ }
        private void AddProduct() { /* thêm mới sản phẩm */ }
        private void EditProduct() { /* sửa sản phẩm */ }
        private void DeleteProduct() { /* xóa sản phẩm */ }
    }
}
