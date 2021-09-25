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
    public class ProvidersController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public ProvidersController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var providers = _db.Providers.ToArray();

            // Make the respone quick, light and small
            var providerDetails = providers.Select(p => new ProviderDetail
            {
                Id = p.Id,
                Country = p.Country,
                Logo = p.Logo,
                Name = p.Name,
                Phone = p.Phone
            });

            return Ok(providerDetails);
        }

        // /api/providers/5445
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var provider = _db.Providers.Find(id);

            if (provider == null)
                return NotFound();

            return Ok(new ProviderDetail
            {
                Id = provider.Id,
                Country = provider.Country,
                Logo = provider.Logo,
                Name = provider.Name,
                Phone = provider.Phone,
                // TODO: Fill the reservations 
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProviderDetail model)
        {
            // HERE ID = 0 
            var provider = new Provider
            {
                Country = model.Country,
                Logo = model.Logo,
                Name = model.Name,
                Phone = model.Phone,
            };

            _db.Providers.Add(provider);
            _db.SaveChanges();

            model.Id = provider.Id;

            return Ok(model);
        }

        [HttpPost("todotask")]
        public IActionResult CreateList([FromBody]CreateToDoTaskListRequest model)
        {
            // LOgic to add a new list 
            return Ok();
        }

    }
}
