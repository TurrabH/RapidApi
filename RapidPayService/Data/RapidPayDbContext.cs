using Microsoft.EntityFrameworkCore;
using RapidPayService.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RapidPayService.Data
{
    public class RapidPayDbContext : DbContext
    {
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : base(options) { }

        public DbSet<CardHolder> CardHolders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
