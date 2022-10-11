using Microsoft.EntityFrameworkCore;

namespace ProjectHangfire.Entity
{
    public class ApplicationDbContext:DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<OnlyDateEntity> OnlyDateEntities { get; set; }
    }
}
