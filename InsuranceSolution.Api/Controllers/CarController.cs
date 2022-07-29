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
    public class CarController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public CarController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]

        public IActionResult Get()
        {
            var cars = _db.Cars.ToArray();

            var carDetails = cars.Select(c => new CarDetails
            {
                Id = c.Id,
                MakeModel = c.MakeModel,
                MaxSpeed = c.MaxSpeed,
                Millage = c.Millage,
                Year = c.Year,
                CustomerId = c.CustomerId
            }).ToArray();

            return Ok(carDetails);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var car = _db.Cars.Find(id);

            if (car == null)
                return NotFound();

            return Ok(new CarDetails
            {
                Id = car.Id,
                MakeModel = car.MakeModel,
                MaxSpeed = car.MaxSpeed,
                Millage = car.Millage,
                Year = car.Year,
                CustomerId = car.CustomerId
            });
        }


        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            var car = _db.Cars.Find(id);
            if (car == null)
                return NotFound();

            _db.Cars.Remove(car);
            _db.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Post([FromBody] CarDetails model)
        {

            // Check if the submittecd customer is valid 
            var customer = _db.Customers.Find(model.CustomerId);
            if (customer == null)
                return NotFound();

            var car = new Car
            {
                MakeModel = model.MakeModel,
                MaxSpeed = model.MaxSpeed,
                Millage = model.Millage,
                Year = model.Year,
                CustomerId = model.CustomerId
            };
            _db.Cars.Add(car);
            _db.SaveChanges();

            model.Id = car.Id;

            return Ok(model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] CarDetails model)
        {
            var customer = _db.Customers.Find(model.CustomerId);
            if (customer == null)
                return NotFound("Customer not found");

            var cars = _db.Cars.Find(model.Id);
            if (cars == null)
                return NotFound();

            cars.Id = model.Id;
            cars.MakeModel = model.MakeModel;
            cars.MaxSpeed = model.MaxSpeed;
            cars.Millage = model.Millage;
            cars.Year = model.Year;
            cars.CustomerId = model.CustomerId;

            _db.SaveChanges();

            return Ok();
        }
    }
}
