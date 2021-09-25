using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSolution.Shared.Models
{
    public class ReservationDetail
    {

        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
        public decimal Price { get; set; } // Devimal(18, 2)
        public int ProviderId { get; set; }
        public int CarId { get; set; }
        
    }
}
