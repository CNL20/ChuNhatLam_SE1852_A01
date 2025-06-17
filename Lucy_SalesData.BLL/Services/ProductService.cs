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
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository _productRepository)
        {
            _productRepository = _productRepository;
        }
        public List<Product> GetAll() => _productRepository.GetAll();
        public Product GetById(int id) => _productRepository.GetById(id);
        public void Add(Product product)
        {
            if (_productRepository.GetAll().Any(p => p.ProductName.ToLower() == product.ProductName.ToLower()))
            {
                throw new System.Exception("Tên sản phẩm đã tồn tại!");
            }
            _productRepository.Add(product);

        }
        public void Update(Product product) => _productRepository.Update(product);
        public void Delete(int id) => _productRepository.Delete(id);
        public List<Product> Search(string key) => _productRepository.Search(key);

    }
}
