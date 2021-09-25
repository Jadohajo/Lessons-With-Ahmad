using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceSolution.Models
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set;  }
        [Required]
        [StringLength(2)]
        public string Country { get; set;  }
        [Required]
        [StringLength(256)]
        public string Logo { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        public List<Reservation> Reversations { get; set; }
    }
}
