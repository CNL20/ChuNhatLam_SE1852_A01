using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChuNhatLamWPF.Helper.ChuNhatLamWPF.ViewModels;
using ChuNhatLamWPF.Helper;
using System.Windows.Input;

namespace ChuNhatLamWPF.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private int _totalOrders;
        public int TotalOrders
        {
            get => _totalOrders;
            set { _totalOrders = value; OnPropertyChanged(); }
        }

        private decimal _totalRevenue;
        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set { _totalRevenue = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { get; }

        public ReportViewModel()
        {
            RefreshCommand = new RelayCommand(_ => LoadReport());
            LoadReport();
        }

        private void LoadReport()
        {
            // Gán giá trị mẫu, thực tế gọi BLL/DAL để lấy dữ liệu
            TotalOrders = 20;
            TotalRevenue = 100000000;
        }
    }
}
