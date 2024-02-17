using HookUpDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HookUpDAL.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<AppUser> Users { get; set; }
    }
}
