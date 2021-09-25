using System;

namespace InsuranceSolution.Shared.Models
{
    public class CustomerDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Phone { get; set; }

        public string Country { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public int Age => Convert.ToInt32((DateTime.Now - Birthdate).TotalDays / 365);

        // TODO: Add the list of cars 
        //public List<CarSummary> Cars { get; set; }
        public int CarsCount { get; set; }
    }
}
