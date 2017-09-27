using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Api.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private Dictionary<int, Product> dataSource;

        public ProductRepository()
        {
            this.dataSource = new Dictionary<int, Product>();
            this.dataSource.Add(1, new Product() { Id = 1, Description = "Product 01", Price = 100});
            this.dataSource.Add(2, new Product() { Id = 2, Description = "Product 02", Price = 200});
            this.dataSource.Add(3, new Product() { Id = 3, Description = "Product 03", Price = 300});
            this.dataSource.Add(4, new Product() { Id = 4, Description = "Product 04", Price = 400});
            this.dataSource.Add(5, new Product() { Id = 5, Description = "Product 05", Price = 500 });
        }

        public Product Get(int id)
        {
            Product result = null;
            this.dataSource.TryGetValue(id, out result);
            return result;
        }

        public Product InsertOrUpdate(Product item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}