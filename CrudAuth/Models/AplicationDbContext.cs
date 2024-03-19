using Microsoft.EntityFrameworkCore;
using CrudAuth.Models.Entities;


namespace CrudAuth.Models
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
