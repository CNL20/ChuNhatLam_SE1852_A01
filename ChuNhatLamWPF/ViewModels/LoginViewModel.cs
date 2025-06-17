using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Lucy_SalesData.BLL.Interfaces;
using Lucy_SalesData.BLL.Services;
using Lucy_SalesData.DAL.Repositories;
using ChuNhatLamWPF.Helper.ChuNhatLamWPF.ViewModels;

namespace ChuNhatLamWPF.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string _userName;
        private string _password;
        private readonly IEmployeeService _employeeService;

        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            // Khởi tạo service, có thể inject qua DI nếu setup
            _employeeService = new EmployeeService(/* truyền repository nếu cần */);
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            var employee = _employeeService.Login(UserName, Password);
            if (employee != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Chuyển sang màn hình chính
                Application.Current.Windows[0].Close();
                new MainWindow().Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
