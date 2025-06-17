using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Singleton;

namespace Lucy_SalesData.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees;

        public EmployeeRepository()
        {
            _employees = DataContext.Instance.Employees;
        }
        public List<Employee> GetAll() => _employees;
        public Employee Login(string username, string password)
        {
           
            return _employees
                .FirstOrDefault(e => e.UserName == username && e.Password == password);
        }
    }
}
