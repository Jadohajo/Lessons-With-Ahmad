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

            var cardetails = cars.Select(c => new Car
            {
                Id = c.Id,
                MakeModel = c.MakeModel,
                MaxSpeed = c.MaxSpeed,
                Millage = c.Millage,
                Year = c.Year,
                CustomerId = c.CustomerId
            });

            return Ok(cardetails);
        }


        [HttpGet("{ID}")]
        public IActionResult Get(int id)
        {
            var carsget = _db.Cars.Find(id);

            if (carsget == null)
                return NotFound();

            return Ok(new Car
            {
                Id = carsget.Id,
                MakeModel = carsget.MakeModel,
                MaxSpeed = carsget.MaxSpeed,
                Millage = carsget.Millage,
                Year = carsget.Year,
                CustomerId = carsget.CustomerId
            });
        }


        [HttpDelete]

        public IActionResult Delete(int id)
        {

            var Cars = _db.Cars.Find(id);
            if (Cars == null)
                return NotFound();

            _db.Cars.Remove(Cars);
            _db.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Post([FromBody] Car model)
        {


            // HERE ID = 0 
            var cardetails = new Car
            {
                Id = model.Id,
                MakeModel = model.MakeModel,
                MaxSpeed = model.MaxSpeed,
                Millage = model.Millage,
                Year = model.Year,
                CustomerId = model.CustomerId
            };

            _db.Cars.Add(cardetails);
            _db.SaveChanges();

            model.Id = cardetails.Id;

            return Ok(model);
        }


        [HttpPut]

        public IActionResult Put([FromBody] Car model)
        {

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
