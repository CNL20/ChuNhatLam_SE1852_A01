using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.BLL.Interfaces;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Repositories;

namespace Lucy_SalesData.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService()
        {
        }

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public List<Employee> GetAll() => _employeeRepository.GetAll();
        public Employee Login (string username, string password)
        {
            var empList = _employeeRepository.GetAll();
            var emp = empList.FirstOrDefault(e => e.UserName.Equals
            (username, System.StringComparison.OrdinalIgnoreCase) && e.Password == password);

            return emp;
        }
    }
}
