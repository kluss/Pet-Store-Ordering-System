using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PetShop.Models;
using PetShop.DTOs;
using PetShop.Data;

namespace PetShop.Services
{
    public class DataServices
    {
        private readonly ShopDbContext _context;

        public DataServices(ShopDbContext context)
        {
            _context = context;
        }

        public FetchProductDTO rawProducts = new FetchProductDTO();

        public async Task GetProductsFromURLAsync()
        {
            // fetch products
            var client = new HttpClient();
                try
                {
                    client.BaseAddress = new Uri("https://vrwiht4anb.execute-api.us-east-1.amazonaws.com");
                    var response = await client.GetAsync($"default/product");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                   var  rawProducts = JsonConvert.DeserializeObject<FetchProductDTO>(stringResult);

                await UpdateDatabaseAsync(rawProducts);
                }
                catch (HttpRequestException httpRequestException)
                {
                    // error fetching items
                }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawProducts"></param>
        /// <returns></returns>
        public async Task UpdateDatabaseAsync(FetchProductDTO rawProducts)
            {

            if (rawProducts.StatusCode == 200)
            {

                //loop through products
                foreach (var item in rawProducts.Body)
                {
                    //check if it exists in the DB  
                    var product = await _context.Products.FindAsync(item.Id);
                    if (product != null)
                    {
                        product.Name = item.Name;
                        product.Price = item.Price;
                        
                        //update data
                        _context.Update(product);
                    }
                    else
                    {
                        //add new item
                        _context.Products.Add(item);
                    }
                }

                await _context.SaveChangesAsync();
            }
           
        }

        /// <summary>
        /// Get Order Total for a given order
        /// </summary>
        /// <param name="order">Order for which we want to get total</param>
        /// <returns>Total value of products </returns>
        public decimal GetOrderTotal(Order order)
        {

            Decimal OrderTotal = 0m;

            foreach (var item in order.Items)
            {
                var product = _context.Products.SingleOrDefault(p => p.Id == item.ProductId);
                OrderTotal = OrderTotal + (decimal)(product.Price * item.Quantity);
            }


            return OrderTotal;
        }

    }
}
