using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Categories;

namespace Publishing.DataAccess
{
    public class PublishingDbContext : DbContext
    {

        public DbSet<ThreadCategory> ThreadCategories { get; set; }
        public PublishingDbContext(DbContextOptions<PublishingDbContext> options)
            : base(options)
        {
        }
    }
}
