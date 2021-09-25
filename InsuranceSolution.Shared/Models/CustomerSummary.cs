using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSolution.Shared.Models
{
    // To retrieve a big list of customers 
    public class CustomerSummary
    {
        public int Id { get; set; } 

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        // Ready-Only 
        public string CountryFlag => $"https://www.countryflags.io/{Country.ToLower()}/shiny/64.png";
    }
}
