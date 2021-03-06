﻿using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Categories;
using Publishing.Domain.Entities.Threads;

namespace Publishing.DataAccess
{
    public class PublishingDbContext : DbContext
    {

        public DbSet<ThreadCategory> ThreadCategories { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<ThreadReply> ThreadReplies { get; set; }
        public DbSet<SeenCount> ThreadSeenCounts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public PublishingDbContext(DbContextOptions<PublishingDbContext> options)
            : base(options)
        {
        }

        public PublishingDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Thread>()
                .HasMany<ThreadReply>(a => a.Replies)
                .WithOne(p => p.Thread)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Thread>()
                .HasMany<SeenCount>(a => a.SeenCount)
                .WithOne(p => p.Thread)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
