using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChuNhatLamWPF.Views;
using ChuNhatLamWPF.ViewModels;


namespace ChuNhatLamWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new CustomerView();
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
            var win = new ReportView();
            win.Show();
        }
    }
}