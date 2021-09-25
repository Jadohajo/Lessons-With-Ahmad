using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSolution.Shared.Models
{
    public class CarDetails
    {   

        public int Id { get; set; }

        public string MakeModel { get; set; }

        public int Year { get; set; }
        public int Millage { get; set; }

        public int MaxSpeed { get; set; }

        public int CustomerId { get; set; }

    }
}
