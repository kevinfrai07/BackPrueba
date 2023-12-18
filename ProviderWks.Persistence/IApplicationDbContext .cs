using Microsoft.EntityFrameworkCore;
using ProviderWks.Domain.Entities;

namespace ProviderWks.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<TblUsers> Users { get; set; }
        DbSet<TblProducto> Producto { get; set; }

        Task<int> SaveChangesAsync();
    }
}
