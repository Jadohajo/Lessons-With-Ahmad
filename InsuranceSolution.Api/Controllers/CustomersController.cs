using InsuranceSolution.Models;
using InsuranceSolution.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceSolution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //data access layer/point
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Retrieve all summary of each customer 

        // THE LEGACY VERSION 
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = _db.Customers.ToArray();

        //    return Ok(customers);
        //}

        // THE OPTIMIZED BETTER VERSION 
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _db.Customers.ToArray();
            // SELECT function in LINQ is used for project 
            var customerSummaries = customers.Select(c => new CustomerSummary
            {
                Id = c.Id,
                Country = c.Country,
                Email = c.Email,
                FullName = $"{c.FirstName} {c.LastName}"
            });

            return Ok(customerSummaries);
        }

        // Retrieve one with all the details 
        // GET: /api/customers/345435
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
                return NotFound();

            // Retrieve the cars for theselected customer 
            var customerCars = _db.Cars.Where(c => c.CustomerId == id).ToArray(); 

            return Ok(new CustomerDetail
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Birthdate = customer.Birthday,
                Email = customer.Email,
                CarsCount = customerCars.Length,
                Cars = customerCars.Select(c => new CarDetails
                {
                    Id = c.Id,
                    Year = c.Year,
                    MakeModel = c.MakeModel,
                    MaxSpeed = c.MaxSpeed,
                    Millage = c.Millage,
                }).ToList()
            });
        }

        // Retrieve customers by country // seach criteria 
        // GET: /api/customers/country?code=UK
        // GET: /api/customers/country/UK
        [HttpGet("country/{country}")]
        public IActionResult Get(string country)
        {
            // Find retrieve one customer by it's primary 
            var customers = _db.Customers.Where(c => c.Country == country).ToArray();

            return Ok(customers);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer model)
        {
            _db.Customers.Add(model);

            _db.SaveChanges();

            return Ok(model.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CustomerDetail model)
        {
            // Fetch teh customer first 
            var customer = _db.Customers.Find(model.Id);
            if (customer == null)
                return NotFound();

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName; 
            customer.Email = model.Email;   
            customer.Phone = model.Phone;
            customer.Country = model.Country;
            customer.Birthday = model.Birthdate;

            _db.SaveChanges();

            return Ok(); 
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // Find compares an object with primary value of the object 
            var customer = _db.Customers.Find(id);
            if (customer == null)
                return NotFound(); 

            _db.Customers.Remove(customer);
            _db.SaveChanges(); 
            return Ok();
        }

    }
}
