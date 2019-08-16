using Microsoft.EntityFrameworkCore;
 
namespace Login.Models
{
    public class LoginContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public LoginContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
    }
}
