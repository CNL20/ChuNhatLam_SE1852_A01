using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ChuNhatLamWPF.Helper;
using System.Linq;
using Lucy_SalesData.DAL.Singleton;
using Lucy_SalesData.DAL.Models;
using ChuNhatLamWPF.Views;
using ChuNhatLamWPF;
using Microsoft.EntityFrameworkCore;

namespace ChuNhatLamWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            try
            {
                // Kết nối tới DbContext
                var context = DbContextFactory.Create();
                var user = context.Employees
                    .AsNoTracking()  // Thêm dòng này để tránh tracking entity
                    .FirstOrDefault(e => e.UserName == Username && e.Password == Password);

                if (user != null)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    // Mở MainWindow
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    
                    // Đóng cửa sổ login
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginView)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}