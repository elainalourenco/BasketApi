using ShoppingCart.Client.Services;
using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Client
{
    class Program
    {
        static HttpClient client = new HttpClient();
        const string baseURL = "http://localhost:60695";

        public static void Main(string[] x)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                ProductsService productsService = new ProductsService(client);
                BasketsService basketsService = new BasketsService(client);
                BasketItemsService basketItemsService = new BasketItemsService(client);

                // First, get a list of all available products
                IList<Product> products = await productsService.GetAllAsync();
                foreach (Product product in products)
                {
                    Console.WriteLine("ProductId={0} - Desc={1} - Price={2}", product.Id, product.Description, product.Price);
                }

                // Second, create a shopping basket whith at least one item
                Basket basket = await basketsService.CreateAsync(
                    new Basket()
                    {
                        Items = new List<BasketItem>() { new BasketItem() { ProductId = products[0].Id, Quantity = 3 } }
                    });
                if (basket == null)
                {
                    Console.WriteLine("Error creating basket. Aborting...");
                    return;
                }
                Console.WriteLine("Basket created");

                // Add a second item to the existing shopping basket
                BasketItem basketItem = await basketItemsService.CreateAsync(new BasketItem() { BasketId = basket.Id, ProductId = products[1].Id, Quantity = 5 });
                if (basketItem == null)
                {
                    Console.WriteLine("Error creating basketItem. Aborting...");
                    return;
                }
                Console.WriteLine("Second product added to basket");

                // Updates the second item in the sopping basket
                basketItem = await basketItemsService.UpdateAsync(new BasketItem() { BasketId = basket.Id, ProductId = products[1].Id, Quantity = 7 });
                if (basketItem == null)
                {
                    Console.WriteLine("Error updating basketItem. Aborting...");
                    return;
                }
                Console.WriteLine("Second product quantity changed");

                await basketItemsService.DeleteAsync(basket.Id, products[0].Id);
                Console.WriteLine("First product removed from basket");

                basket = await basketsService.GetAsync(basket.Id);
                PrintBasket(basket);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static void PrintBasket(Basket basket)
        {
            Console.WriteLine();
            Console.WriteLine("Basket ID={0}", basket.Id);
            foreach (BasketItem item in basket.Items)
            {
                Console.WriteLine("ProductId={0} Quantity={1}", item.ProductId, item.Quantity);
            }
        }

    }
}
