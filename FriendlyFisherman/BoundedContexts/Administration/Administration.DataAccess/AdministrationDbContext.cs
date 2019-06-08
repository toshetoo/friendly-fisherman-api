using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using Administration.Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccess
{
    public class AdministrationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<EventComment> EventComments { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollAnswer> PollAnswers { get; set; }
        public DbSet<UserPollAnswer> UserPollAnswers { get; set; }

        public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options)
            : base(options)
        {
        }

        public AdministrationDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Poll>()
                .HasMany<PollAnswer>(a => a.Answers)
                .WithOne(p => p.Poll)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
