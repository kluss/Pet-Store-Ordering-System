using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.DTOs;
using PetShop.Models;
using Newtonsoft.Json;
using PetShop.Services;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly DataServices _service;

        public OrdersController(DataServices service)
        {
            _service = service;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        //[ProducesResponseType(201, Type = typeof(Order))]
        //[ProducesResponseType(404)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await  _context.Orders.Include("Items").SingleOrDefaultAsync(i=>i.OrderId==id);

            if (order == null)
            {
                return NotFound();
            }
            //Get order total            
            var orderTotal = _service.GetOrderTotal(order);

            //foreach (var item in order.Items)
            //{
            //    var product= _context.Products.SingleOrDefault(p => p.Id == item.ProductId);
            //     OrderTotal = OrderTotal + (decimal)(product.Price * item.Quantity );
            //}

            var result = new OrderDto { Order = order, OrderTotalCost = orderTotal };
          
        
          // return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));

            return Ok(result);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest();

            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
