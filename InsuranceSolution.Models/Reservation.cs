using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceSolution.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Column("StartDate", TypeName = "date")]
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }

        
        [Column("Price", TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } // Devimal(18, 2)

        public Provider provider { get; set; }
        [ForeignKey(nameof(provider))]
        public int ProviderId { get; set; }

        public Car car { get; set; }
        [ForeignKey(nameof(car))]
        public int CarId { get; set; }


    }

}
