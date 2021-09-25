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
    public class ReservationsController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public ReservationsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var Reservations = _db.Reservations.ToArray();

            // Make the respone quick, light and small
            var ReservationsDetail = Reservations.Select(p => new ReservationSummary
            {
                Id = p.Id,
                Price = p.Price,
                //CarId = p.CarId,
                //ProviderId = p.ProviderId,
                StartDate = p.StartDate,
                //Enddate = p.Enddate
            });

            return Ok(Reservations);
        }


        [HttpGet("{ID}")]
        public IActionResult Get(int Id)
        {

            var reservation = _db.Reservations.Find(Id);

            if (reservation == null)
                return NotFound();


            //Less detail due to not needing the extra information if just looking for an ID
            //Could look for a reservation ID in a get that then pulls up all of the car detail??
            return Ok(new Reservation
            {

                Id = reservation.Id,
                Price = reservation.Price,
                CarId = reservation.CarId,
                ProviderId = reservation.ProviderId,

            });

        }


        [HttpPut]
        public IActionResult Put([FromBody] ReservationDetail model)
        {


            var Reservation = _db.Reservations.Find(model.Id);
            if (Reservation == null)
                return NotFound();

            Reservation.Id = model.Id;
            Reservation.Price = model.Price;
            Reservation.CarId = model.CarId;
            Reservation.ProviderId = model.CarId;
            Reservation.StartDate = model.StartDate;
            Reservation.Enddate = model.Enddate;


            _db.SaveChanges();


            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var Reservation = _db.Reservations.Find(id);
            if (Reservation == null)
                return NotFound();


            _db.Reservations.Remove(Reservation);
            _db.SaveChanges();


            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReservationDetail model)
        {
            // HERE ID = 0 
            var Reservations = new Reservation
            {
                Id = model.Id,
                Price = model.Price,
                StartDate = model.StartDate,
                Enddate = model.Enddate,
                CarId = model.CarId,
                ProviderId = model.ProviderId
            };

            _db.Reservations.Add(Reservations);
            _db.SaveChanges();

            model.Id = Reservations.Id;

            return Ok(model);
        }

    }
}
