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
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public List<Employee> GetAll() => _context.Employees.ToList();
        public Employee Login(string username, string password)
        {
           
            return _context.Employees
                .FirstOrDefault(e => e.UserName == username && e.Password == password);
        }
    }
}
