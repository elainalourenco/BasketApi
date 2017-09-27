using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Client.Services
{
    public class BasketItemsService
    {
        private HttpClient client;

        public BasketItemsService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<BasketItem> CreateAsync(BasketItem value)
        {
            BasketItem result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/BasketItems/", value);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<BasketItem>();
            }
            return result;
        }

        public async Task<BasketItem> UpdateAsync(BasketItem value)
        {
            BasketItem result = null;
            HttpResponseMessage response = await client.PutAsJsonAsync("api/BasketItems/", value);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<BasketItem>();
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int parentId, int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(string.Format("api/BasketItems/{0}/{1}", parentId, id));
            return response.IsSuccessStatusCode;
        }
    }
}
