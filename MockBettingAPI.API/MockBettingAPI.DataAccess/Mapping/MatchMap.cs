using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MockBettingAPI.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockBettingAPI.DataAccess.Mapping
{
    internal class MatchMap : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");
            builder.HasKey(k => k.ID);

            builder.HasMany(m => m.MatchOdds)
                .WithOne(m => m.Match)
                .HasForeignKey(k => k.MatchID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
