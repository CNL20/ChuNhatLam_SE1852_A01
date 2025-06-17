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
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        public ProductRepository()
        {
            _products = DataContext.Instance.Products;
        }

        public List<Product> GetAll() => _products;
        public Product GetById(int id) => _products.FirstOrDefault(p => p.ProductID == id);

        public void Add(Product product)
        {
            product.ProductID = _products.Count > 0 ? _products.Max(p => p.ProductID) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var pro = GetById(product.ProductID);
            if (pro != null)
            {
                pro.ProductName = product.ProductName;
                pro.CategoryID = product.CategoryID;
                pro.UnitPrice = product.UnitPrice;
                pro.UnitsInStock = product.UnitsInStock;
            }
        }

        public void Delete(int id)
        {
            var pro = GetById(id);
            if (pro != null)
            {
                _products.Remove(pro);
            }
        }

        public List<Product> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _products;
            }
            else
            {
                return _products.Where(p => p.ProductName.ToLower().Contains(key.ToLower())).ToList();
            }
        }
    }
}    