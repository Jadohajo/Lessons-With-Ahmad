using System;

namespace InsuranceSolution.Shared.Models
{
    public class ReservationSummary
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MakeModel { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public string ProviderName { get; set; }
    }
}
