using System;
using System.ComponentModel;
using System.Windows.Input;
using ChuNhatLamWPF.Helper;
using Lucy_SalesData.BLL.Interfaces;

namespace ChuNhatLamWPF.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private readonly IReportService _reportService;

        private DateTime _fromDate = DateTime.Today;
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                OnPropertyChanged(nameof(FromDate));
            }
        }

        private DateTime _toDate = DateTime.Today;
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged(nameof(ToDate));
            }
        }

        private decimal _totalRevenue;
        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                _totalRevenue = value;
                OnPropertyChanged(nameof(TotalRevenue));
            }
        }

        public ICommand RefreshCommand { get; private set; }

        public ReportViewModel(IReportService reportService)
        {
            _reportService = reportService;
            RefreshCommand = new RelayCommand(ExecuteRefresh);
            LoadReport();
        }

        private void ExecuteRefresh(object parameter)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            TotalRevenue = _reportService.GetRevenue(FromDate, ToDate);
            // Nếu muốn các báo cáo khác, thêm property và gọi hàm tương ứng của _reportService
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}