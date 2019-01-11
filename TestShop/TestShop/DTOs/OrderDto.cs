using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.DTOs
{
    public class OrderDto
    {
        public Order Order { get; set; }
        public Decimal OrderTotalCost { get; set; }
    }
}
