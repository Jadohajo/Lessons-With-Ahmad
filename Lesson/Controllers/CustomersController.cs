using Lesson.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Lesson.Controllers
{
    [ApiController]
    [Route("Api.v1.0./[Controller]")]
    public class CustomersController : ControllerBase
    {


        private static List<Customer> _customers = new List<Customer>();
        
        [HttpGet]

        public IActionResult Get()
        {
            return Ok(_customers); //2000
        }


        [HttpPost]

        public IActionResult Post([FromBody] Customer model)
        {
            if (model.Emaill == null)
                return BadRequest("Email is Required"); //status 400
            _customers.Add(model);

            return Ok();

             // return CreatedAtAction("Post", model); // Status 201

        }

        [HttpPut]

        public IActionResult Put([FromBody]Customer model)
        {
            var existingCustomer = _customers.SingleOrDefault(c => c.Id == model.Id);

            if (existingCustomer == null)
                return NotFound(); // 404

            existingCustomer.Emaill = model.Emaill;
            existingCustomer.FullName = model.FullName;
            existingCustomer.Id = model.Id;
            existingCustomer.Birthdate = model.Birthdate;

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existingCustomer = _customers.SingleOrDefault(c => c.Id == id);

            if (existingCustomer == null)
                return NotFound();

            _customers.Remove(existingCustomer);
                return Ok();

        }

    }
}
