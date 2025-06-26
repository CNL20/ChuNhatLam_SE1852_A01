using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ChuNhatLamWPF.Helper;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Interfaces;

namespace ChuNhatLamWPF.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly IProductRepository _productRepository;

        public ObservableCollection<Product> Products { get; set; }
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
                // Khi chọn một sản phẩm, tự động đẩy dữ liệu lên UI field
                if (_selectedProduct != null)
                {
                    ProductNameInput = _selectedProduct.ProductName;
                    SupplierIDInput = _selectedProduct.SupplierID;
                    CategoryIDInput = _selectedProduct.CategoryID.ToString();
                    QuantityPerUnitInput = _selectedProduct.QuantityPerUnit;
                    UnitPriceInput = _selectedProduct.UnitPrice;
                    UnitsInStockInput = _selectedProduct.UnitsInStock;
                    UnitsOnOrderInput = _selectedProduct.UnitsOnOrder;
                    ReorderLevelInput = _selectedProduct.ReorderLevel;
                    DiscontinuedInput = _selectedProduct.Discontinued;
                }
            }
        }

        // Các property để binding với TextBox trong UI
        private string _productNameInput;
        public string ProductNameInput
        {
            get => _productNameInput;
            set { _productNameInput = value; OnPropertyChanged(); }
        }
        private int _supplierIDInput;
        public int SupplierIDInput
        {
            get => _supplierIDInput;
            set { _supplierIDInput = value; OnPropertyChanged(); }
        }
        private string _categoryIDInput;
        public string CategoryIDInput
        {
            get => _categoryIDInput;
            set { _categoryIDInput = value; OnPropertyChanged(); }
        }
        private string _quantityPerUnitInput;
        public string QuantityPerUnitInput
        {
            get => _quantityPerUnitInput;
            set { _quantityPerUnitInput = value; OnPropertyChanged(); }
        }
        private decimal _unitPriceInput;
        public decimal UnitPriceInput
        {
            get => _unitPriceInput;
            set { _unitPriceInput = value; OnPropertyChanged(); }
        }
        private int _unitsInStockInput;
        public int UnitsInStockInput
        {
            get => _unitsInStockInput;
            set { _unitsInStockInput = value; OnPropertyChanged(); }
        }
        private int _unitsOnOrderInput;
        public int UnitsOnOrderInput
        {
            get => _unitsOnOrderInput;
            set { _unitsOnOrderInput = value; OnPropertyChanged(); }
        }
        private int _reorderLevelInput;
        public int ReorderLevelInput
        {
            get => _reorderLevelInput;
            set { _reorderLevelInput = value; OnPropertyChanged(); }
        }
        private bool _discontinuedInput;
        public bool DiscontinuedInput
        {
            get => _discontinuedInput;
            set { _discontinuedInput = value; OnPropertyChanged(); }
        }

        private string _searchKey;
        public string SearchKey
        {
            get => _searchKey;
            set { _searchKey = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Products = new ObservableCollection<Product>();
            LoadProducts();

            AddCommand = new RelayCommand(_ => AddProduct());
            EditCommand = new RelayCommand(_ => EditProduct(), _ => SelectedProduct != null);
            DeleteCommand = new RelayCommand(_ => DeleteProduct(), _ => SelectedProduct != null);
            RefreshCommand = new RelayCommand(_ => LoadProducts());
            SearchCommand = new RelayCommand(_ => Search());
        }

        private void LoadProducts()
        {
            try
            {
                Products.Clear();
                foreach (var product in _productRepository.GetAll())
                    Products.Add(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}");
            }
        }

        private void AddProduct()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ProductNameInput))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(CategoryIDInput))
                {
                    MessageBox.Show("Vui lòng nhập mã chủng loại!");
                    return;
                }
                var result = MessageBox.Show(
                            "Bạn có chắc muốn thêm sản phẩm này không?",
                            "Xác nhận thêm sản phẩm",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                var newProduct = new Product
                {
                    ProductName = this.ProductNameInput,
                    SupplierID = this.SupplierIDInput,
                    CategoryID = int.Parse(this.CategoryIDInput),
                    QuantityPerUnit = this.QuantityPerUnitInput,
                    UnitPrice = this.UnitPriceInput,
                    UnitsInStock = this.UnitsInStockInput,
                    UnitsOnOrder = this.UnitsOnOrderInput,
                    ReorderLevel = this.ReorderLevelInput,
                    Discontinued = this.DiscontinuedInput
                };

                _productRepository.Add(newProduct);
                LoadProducts();
                ClearInputs();
                MessageBox.Show("Đã thêm sản phẩm mới!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        private void EditProduct()
        {
            try
            {
                if (SelectedProduct == null) return;

                if (string.IsNullOrWhiteSpace(ProductNameInput))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(CategoryIDInput))
                {
                    MessageBox.Show("Vui lòng nhập mã chủng loại!");
                    return;
                }

                SelectedProduct.ProductName = this.ProductNameInput;
                SelectedProduct.SupplierID = this.SupplierIDInput;
                SelectedProduct.CategoryID = int.Parse(this.CategoryIDInput);
                SelectedProduct.QuantityPerUnit = this.QuantityPerUnitInput;
                SelectedProduct.UnitPrice = this.UnitPriceInput;
                SelectedProduct.UnitsInStock = this.UnitsInStockInput;
                SelectedProduct.UnitsOnOrder = this.UnitsOnOrderInput;
                SelectedProduct.ReorderLevel = this.ReorderLevelInput;
                SelectedProduct.Discontinued = this.DiscontinuedInput;

                _productRepository.Update(SelectedProduct);
                LoadProducts();
                ClearInputs();
                MessageBox.Show($"Đã sửa sản phẩm: {SelectedProduct.ProductName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa sản phẩm: {ex.Message}");
            }
        }

        private void DeleteProduct()
        {
            try
            {
                if (SelectedProduct == null) return;

                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sản phẩm '{SelectedProduct.ProductName}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var name = SelectedProduct.ProductName;
                    _productRepository.Delete(SelectedProduct.ProductID);
                    LoadProducts();
                    ClearInputs();
                    MessageBox.Show($"Đã xóa sản phẩm: {name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}");
            }
        }

        private void Search()
        {
            Products.Clear();
            foreach (var product in _productRepository.Search(SearchKey))
                Products.Add(product);
        }

        private void ClearInputs()
        {
            ProductNameInput = string.Empty;
            SupplierIDInput = 0;
            CategoryIDInput = string.Empty;
            QuantityPerUnitInput = string.Empty;
            UnitPriceInput = 0;
            UnitsInStockInput = 0;
            UnitsOnOrderInput = 0;
            ReorderLevelInput = 0;
            DiscontinuedInput = false;
            SelectedProduct = null;
        }
    }
}