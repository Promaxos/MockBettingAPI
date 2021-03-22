using Microsoft.EntityFrameworkCore;
using MockBettingAPI.Data;
using MockBettingAPI.DataAccess.Mapping;
using System;

namespace MockBettingAPI.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        #region DbSets
        public DbSet<Match> Matches { get; private set; }
        public DbSet<MatchOdds> MatchOdds { get; private set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MatchMap());
            modelBuilder.ApplyConfiguration(new MatchOddsMap());
        }
    }
}
