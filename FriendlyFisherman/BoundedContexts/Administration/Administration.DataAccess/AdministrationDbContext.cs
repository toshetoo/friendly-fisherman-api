﻿using Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccess
{
    public class AdministrationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options)
            : base(options)
        {
        }
    }
}
