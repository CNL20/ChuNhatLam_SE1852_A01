using System.Windows;
using ChuNhatLamWPF.ViewModels;
using Lucy_SalesData.BLL.Interfaces;

namespace ChuNhatLamWPF.Views
{
    public partial class ReportView : Window
    {
        public ReportView(IReportService reportService)
        {
            InitializeComponent();
            DataContext = new ReportViewModel(reportService);
        }
    }
}