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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetAll() => _categoryRepository.GetAll();
        public Category GetById(int id) => _categoryRepository.GetById(id);
        public void Add(Category category)
        {
            if (_categoryRepository.GetAll().Any(c => c.CategoryName.ToLower() == category.CategoryName.ToLower()))
            {
                throw new System.Exception("Tên danh mục đã tồn tại!");
            }
            _categoryRepository.Add(category);
        }
        public void Update(Category category) => _categoryRepository.Update(category);
        public void Delete(int id) => _categoryRepository.Delete(id);
        public List<Category> Search(string key) => _categoryRepository.Search(key);
    }
}
