using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Data;
using PetShop.Services;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly DataServices _service;

        public ValuesController(DataServices service)
        {
            _service = service;
        }

        //CRUD  Create(POST) Read Udpate(PUT) DELETE

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        {

           await _service.GetProductsFromURLAsync();

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value " + id;
        }

        // POST api/values
        //Create
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        //update
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
