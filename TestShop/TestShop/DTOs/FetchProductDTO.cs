using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.DTOs
{
    public class FetchProductDTO
    {
        //public FetchProductDTO()
        //{
        //    products = new List<Product>();
        //}

        public IEnumerable<Product> Body { get; set; }
        public int StatusCode { get; set; }
    }
}
