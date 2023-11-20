using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RapidPayService.EntityFramework.Entities;

namespace RapidPayService.EntityFramework.Data
{
    public class RapidPayDbContext : IdentityDbContext<IdentityUser>
    {
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : base(options) { }

        public DbSet<CardHolder> CardHolders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.CardHolder)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<CardHolder>(ch => ch.UserId)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .HasOne(c => c.CardHolder)
                .WithMany(ch => ch.Cards)
                .HasForeignKey(c => c.CardHolderId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Card)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CardId);
        }
    }

}
