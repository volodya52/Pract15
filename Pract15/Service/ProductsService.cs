using Microsoft.EntityFrameworkCore;
using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract15.Service
{
    public class ProductsService
    {
        private readonly Pract15DatabaseContext _db = DbService.Instance.Context;
        public ObservableCollection<Product> Products { get; set; } = new( );

        public ProductsService ()
        {
            GetAll( );
        }

        public void Add (Product product)
        {
            var _product = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Rating = product.Rating,
                CreatedAt = product.CreatedAt,
                Brand = product.Brand,
                Category = product.Category,
                Tags = product.Tags
            };
            _db.Products.Add(_product);
            Commit( );
        }

        public int Commit () => _db.SaveChanges( );

        public void GetAll ()
        {
            var products = _db.Products
                .Include(s => s.Brand)
                .Include(s => s.Tags)
                .Include(s => s.Category)
                .ToList( );
            Products.Clear( );
            foreach(var product in products)
            {
                Products.Add(product);
            }

        }

        public void Remove(Product product)
        {
            _db.Remove<Product>(product);
            if (Commit( ) > 0)
            {
                if (Products.Contains(product))
                    Products.Remove(product);
            }
        }
    }
}
