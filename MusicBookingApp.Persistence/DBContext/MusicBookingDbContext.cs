using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Persistence.DBContext
{
    public class MusicBookingDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicBookingDbContext(DbContextOptions<MusicBookingDbContext> options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId);

            builder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            builder.Entity<Event>()
           .HasOne(e => e.Artist)
           .WithMany(a => a.Events)
           .HasForeignKey(e => e.ArtistId);

            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId);

            builder.Entity<Payment>()
               .Property(p => p.Amount)
               .HasColumnType("decimal(18,2)");
        }
    }

}
