using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Client.Services
{
    public class BasketsService
    {
        private HttpClient client;

        public BasketsService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Basket> GetAsync(int id)
        {
            Basket result = null;
            HttpResponseMessage response = await client.GetAsync(string.Format("api/baskets/{0}", id));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Basket>();
            }
            return result;
        }

        public async Task<Basket> CreateAsync(Basket value)
        {
            Basket result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/baskets", value);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Basket>();
            }
            return result;
        }

        public async Task<Basket> UpdateAsync(Basket value)
        {
            throw new NotImplementedException();
        }

        public async void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
