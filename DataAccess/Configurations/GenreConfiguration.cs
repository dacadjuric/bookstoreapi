using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(x => x.GenreName).IsRequired();
            builder.Property(x => x.GenreName).HasMaxLength(25);

            builder.HasMany(x => x.GenreBooks)
               .WithOne(x => x.Genre)
               .HasForeignKey(x => x.GenreId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
