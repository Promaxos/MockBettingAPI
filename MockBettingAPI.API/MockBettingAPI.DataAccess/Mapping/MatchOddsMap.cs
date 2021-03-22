using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MockBettingAPI.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockBettingAPI.DataAccess.Mapping
{
    internal class MatchOddsMap : IEntityTypeConfiguration<MatchOdds>
    {
        public void Configure(EntityTypeBuilder<MatchOdds> builder)
        {
            builder.ToTable("MatchOdds");
            builder.HasKey(k => k.ID);

            builder.Property(o => o.Odd)
                .HasColumnType("decimal(19,2)");
        }
    }
}
