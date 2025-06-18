using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lucy_SalesData.BLL.Interfaces;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Repositories;

namespace Lucy_SalesData.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void AddCustomer(Customer customer)
        {
            if (_customerRepository.GetAll().Any(c => c.CompanyName.ToLower() == customer.CompanyName.ToLower()))            
                throw new System.Exception("Tên Công Ty đã tồn tại!");
                _customerRepository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.Delete(id);
        }

        public List<Customer> Search(string key) => _customerRepository.Search(key);
    }
}
