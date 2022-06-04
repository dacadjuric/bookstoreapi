using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(85);

            builder.Property(x => x.ISBN).IsRequired();
            builder.HasIndex(x => x.ISBN).IsUnique();
            builder.Property(x => x.ISBN).HasMaxLength(15);

            builder.Property(x => x.PublishedDate).IsRequired();

            builder.Property(x => x.TotalPages).IsRequired();

            builder.HasMany(x => x.BookAuthors)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.BookGenres)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);
        }
    }
}
