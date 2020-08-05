using CustomeModule.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomeModule.Model.Model
{
    public class CustomerModuleContext : DbContext
    {
        public CustomerModuleContext(DbContextOptions<CustomerModuleContext> options) : base(options) { }

        // User
        public DbSet<Module> Modules { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }           
        public DbSet<UserRight> UserRights { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

    }
}