using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceSolution.Models
{
    public class Car
    {
        public int Id { get; set; }
        [StringLength(80)] //Max 80 Char
        [Required] // Not null - User has to use
        public string MakeModel { get; set; }

        // if we want int to be nullable add ?
        public int MaxSpeed { get; set; }

        public int Millage { get; set; }

        public int Year { get; set; }

        //Navigation Property
        public Customer customer { get; set; }

        //ot normal int it's a fk for the customer
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; } // FK for the customer table

        public List<Reservation> Reservations { get; set; }
    }
}
