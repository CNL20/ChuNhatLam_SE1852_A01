using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Singleton;
using System.Data;

namespace Lucy_SalesData.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories;
        public CategoryRepository()
        {
            _categories = DataContext.Instance.Categories;
        }
        public List<Category> GetAll() => _categories;
        public Category GetById(int id) => _categories.FirstOrDefault(c => c.CategoryID == id);
        public void Add(Category category)
        {
            category.CategoryID = _categories.Count > 0 ? _categories.Max(c => c.CategoryID) + 1 : 1;
            _categories.Add(category);
        }
        public void Update(Category category)
        {
            var cat = GetById(category.CategoryID);
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;
                cat.Description = category.Description;
            }
        }
        public void Delete(int id)
        {
            var cat = GetById(id);
            if (cat != null)
            {
                _categories.Remove(cat);
            }
        }
        public List<Category> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _categories;
            }
            else
            {
                return _categories.Where(c => c.CategoryName != null && c.CategoryName.ToLower().Contains(key.ToLower())).ToList();
            }
        }
    }
}
