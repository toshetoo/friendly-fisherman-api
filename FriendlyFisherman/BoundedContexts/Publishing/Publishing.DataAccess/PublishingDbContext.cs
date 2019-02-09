using Microsoft.EntityFrameworkCore;

namespace Publishing.DataAccess
{
    public class PublishingDbContext : DbContext
    {
        public PublishingDbContext(DbContextOptions<PublishingDbContext> options)
            : base(options)
        {
        }
    }
}
