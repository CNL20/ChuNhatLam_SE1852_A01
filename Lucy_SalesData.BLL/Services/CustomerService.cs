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

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public List<Customer> GetAll() => _customerRepository.GetAll();
        public Customer GetById(int id) => _customerRepository.GetById(id);
        public void Add(Customer customer)
        {
            if (_customerRepository.GetAll().Any(c => c.CompanyName.ToLower() == customer.CompanyName.ToLower()))            
                throw new System.Exception("Tên Công Ty đã tồn tại!");
            
            if (string.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Bạn phải nhập số, không được để trống hay nhập chữ!");

            if (!Regex.IsMatch(customer.Phone, @"^\d{10}$"))
                throw new ArgumentException("Số điện thoại phải đúng 10 chữ số!");

            _customerRepository.Add(customer);
        }
        public void Update(Customer customer) => _customerRepository.Update(customer);
        public void Delete(int id) => _customerRepository.Delete(id);
        public List<Customer> Search(string key) => _customerRepository.Search(key);
    }
}
