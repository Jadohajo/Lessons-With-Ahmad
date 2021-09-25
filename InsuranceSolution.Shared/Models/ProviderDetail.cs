namespace InsuranceSolution.Shared.Models
{
    /// <summary>
    /// Model that represents the general information of the provider entity to be retireved as a collection
    /// </summary>
    public class ProviderDetail
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Country { get; set; }
        
        public string Logo { get; set; }
        
        public string Phone { get; set; }

        // TODO: Adding the list of reservations 
    }

    public class CreateToDoTaskListRequest
    {
        public string DisplayName { get; set; }
    }
}
