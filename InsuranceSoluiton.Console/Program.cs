using InsuranceSolution.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace InsuranceSoluiton.ConsoleClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CreateCustomerAsync();
            await GetCustomersAsync();

            Console.ReadKey();
            Console.WriteLine("Press any key to continue;");

        }

        #region GET Request
        private static async Task GetCustomersAsync()
        {
            var httpClient = new HttpClient();
            var customers = await httpClient.GetFromJsonAsync<CustomerSummary[]>("https://localhost:44351/api/customers");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id} - {customer.FullName}");
            }
        }
        #endregion 
        #region POST Requst 
        private static async Task CreateCustomerAsync()
        {
            Console.WriteLine("Insert a new customer");
            Console.WriteLine("FirstName:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Phone:");
            string phone = Console.ReadLine();
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Country:");
            string country = Console.ReadLine();
            Console.WriteLine("Birthdate:");
            string birthdate = Console.ReadLine();

            var customer = new CustomerDetail
            {
                FirstName = firstname,
                LastName = lastName,
                Phone = phone,
                Email = email,
                Country = country,
                Birthdate = DateTime.Parse(birthdate)
            };

            // Old Manual way 
            //var content = new StringContent(JsonSerializer.Serialize(customer));
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //var response = await httpClient.PostAsync("https://localhost:44351/api/customers", content);

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("https://localhost:44351/api/customers", customer);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Customer has been added successfully");
            }
            else
            {
                Console.WriteLine("Failed to insert the customer");
            }
        }
        #endregion 
    }
}
