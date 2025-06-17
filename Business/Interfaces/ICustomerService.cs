using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.BLL.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
        List<Customer> Search(string key);
    }
}
