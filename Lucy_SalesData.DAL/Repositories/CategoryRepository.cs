using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy_SalesData.DAL.Models;
using Lucy_SalesData.DAL.Interfaces;
using Lucy_SalesData.DAL.Singleton;

namespace Lucy_SalesData.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public List<Category> GetAll() => _context.Categories.ToList();

        public Category GetById(int id) => _context.Categories.Find(id);

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            var cat = _context.Categories.Find(category.CategoryID);
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;
                cat.Description = category.Description;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
            }
        }

        public List<Category> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _context.Categories.ToList();
            }
            else
            {
                return _context.Categories
                    .Where(c => c.CategoryName != null && c.CategoryName.ToLower().Contains(key.ToLower()))
                    .ToList();
            }
        }
    }
}