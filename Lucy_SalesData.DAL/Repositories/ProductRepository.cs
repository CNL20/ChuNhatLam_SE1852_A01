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
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public List<Product> GetAll() => _context.Products.ToList();

        public Product GetById(int id) => _context.Products.Find(id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var pro = _context.Products.Find(product.ProductID);
            if (pro != null)
            {
                pro.ProductName = product.ProductName;
                pro.SupplierID = product.SupplierID;
                pro.CategoryID = product.CategoryID;
                pro.QuantityPerUnit = product.QuantityPerUnit;
                pro.UnitPrice = product.UnitPrice;
                pro.UnitsInStock = product.UnitsInStock;
                pro.UnitsOnOrder = product.UnitsOnOrder;
                pro.ReorderLevel = product.ReorderLevel;
                pro.Discontinued = product.Discontinued;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var pro = _context.Products.Find(id);
            if (pro != null)
            {
                _context.Products.Remove(pro);
                _context.SaveChanges();
            }
        }

        public List<Product> Search(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _context.Products.ToList();
            }
            else
            {
                return _context.Products
                    .Where(p => p.ProductName.ToLower().Contains(key.ToLower()))
                    .ToList();
            }
        }
    }
}