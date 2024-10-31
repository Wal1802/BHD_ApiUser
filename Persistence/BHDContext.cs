using BHD.Domain.Models;
using BHD.Models.Models;
using BHD.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BHD.Persistence
{
    public class BHDContext : DbContext
    {
        public BHDContext(DbContextOptions<BHDContext> options): base(options)
        {
                
        }

        #region tablas
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
