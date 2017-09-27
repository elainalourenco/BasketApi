using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingCart.Api.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Product
        public IEnumerable<Product> Get()
        {
            return new Product[] { 
                new Product() { Id = 1, Description = "Product 01", Price = 100},
                new Product() { Id = 2, Description = "Product 02", Price = 200},
                new Product() { Id = 3, Description = "Product 03", Price = 300},
                new Product() { Id = 4, Description = "Product 04", Price = 400},
                new Product() { Id = 5, Description = "Product 05", Price = 500}
                };
        }

        // GET: api/Product/5
        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Product
        public void Post(Product value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Product/5
        public void Put(int id, Product value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
