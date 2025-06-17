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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;
        public CustomerRepository()
        {
            _customers = DataContext.Instance.Customers;
        }
        public List<Customer> GetAll() => _customers;
        public Customer GetById(int id) => _customers.FirstOrDefault(c => c.CustomerID == id);
        public void Add(Customer customer)
        {
            customer.CustomerID = _customers.Count > 0 ? _customers.Max(c => c.CustomerID) + 1 : 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var cus = GetById(customer.CustomerID);
            if(cus != null)
            {
                cus.CompanyName = customer.CompanyName;
                cus.ContactName = customer.ContactName;
                cus.ContactTitle = customer.ContactTitle;
                cus.Address = customer.Address;
                cus.Phone = customer.Phone;
            }
        }

        public void Delete(int id)
        {
            var cus = GetById(id);
            if(cus != null)
            {
                _customers.Remove(cus);
            }
        }

        public List<Customer> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _customers;
            }
            else
            {
                return _customers.Where(c => (c.CompanyName != null && c.CompanyName.ToLower().Contains(key.ToLower()))
                                        || (c.Phone != null && c.Phone.Contains(key))).ToList();

            }

        }
    }
}
