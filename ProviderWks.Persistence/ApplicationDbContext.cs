using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.Entities;
using System.Threading.Tasks;

namespace ProviderWks.Persistence
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<TblUsers> Users { get; set; }
        public virtual DbSet<TblProducto> Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUsers>().ToTable("Users");
            modelBuilder.Entity<TblProducto>().ToTable("Producto");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {



        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
