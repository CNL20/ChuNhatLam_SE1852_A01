using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;

namespace Lucy_SalesData.BLL.Interfaces
{
    public interface ICategoryService
    {
        public List<Category> GetAll();
        public Category GetById(int id);
        public void Add(Category category);
        public void Update(Category category);
        public void Delete(int id);
        public List<Category> Search(string key);
    }
}
