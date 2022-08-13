using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSolution.Models
    // Default AspNet Identity user called (IdentityUser) & default role object called IdentityRole 
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Each Dbset Property represents a table
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Provider> Providers { get; set; }

    }
}

//public class MyUser : IdentityUser
//{
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//}