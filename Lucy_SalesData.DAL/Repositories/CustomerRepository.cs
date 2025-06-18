using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Lucy_SalesData.DAL.Singleton;

namespace Lucy_SalesData.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll() => _context.Customers.ToList();

        public Customer GetById(int id) => _context.Customers.Find(id);

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var existing = _context.Customers.Find(customer.CustomerID);
            if (existing != null)
            {
                existing.CompanyName = customer.CompanyName;
                existing.ContactName = customer.ContactName;
                existing.Phone = customer.Phone;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var existing = _context.Customers.Find(id);
            if (existing != null)
            {
                _context.Customers.Remove(existing);
                _context.SaveChanges();
            }
        }

        public List<Customer> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _context.Customers.ToList();
            }
            else
            {
                return _context.Customers
                    .Where(c => (c.CompanyName != null && c.CompanyName.ToLower().Contains(key.ToLower()))
                             || (c.Phone != null && c.Phone.Contains(key)))
                    .ToList();
            }
        }
    }
}