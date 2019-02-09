using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccess
{
    public class AdministrationDbContext : DbContext
    {
        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options)
            : base(options)
        {
        }
    }
}
