using Microsoft.EntityFrameworkCore;

namespace Souk.Infrastructure.Data;

public class DbContext(DbContextOptions<DbContext> options)
    : DbContext(options)
{
    public DbSet<Vendor> Vendors => Set<Vendor>();

}

