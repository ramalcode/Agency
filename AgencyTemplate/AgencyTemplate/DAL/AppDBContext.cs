using AgencyTemplate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgencyTemplate.DAL
{
    public class AppDBContext:IdentityDbContext
    {
        public AppDBContext (DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }

    }
}
