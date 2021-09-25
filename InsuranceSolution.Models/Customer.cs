using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsuranceSolution.Models
{

    //business models
    //domain models
   public class Customer
    {

        //This is a key and it's a primarty key not null and no repeatable

        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [StringLength(25)]
        public string Email { get; set; }
        [Required]
        [StringLength(25)]
        public string Country { get; set;  }

        public DateTime Birthday { get; set; }
        
        // Relationships 
        public List<Car> cars { get; set; }

    }

}
