using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Client.Services
{
    public class ProductsService
    {
        private HttpClient client;

        public ProductsService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            IList<Product> result = new List<Product>();
            HttpResponseMessage response = await client.GetAsync("api/products/");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<IList<Product>>();
            }
            return result;
        }
    }
}
