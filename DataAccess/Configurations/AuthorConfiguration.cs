using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(20);
            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(20);
            builder.Property(x => x.LastName)
                .IsRequired();

            builder.Property(x => x.Username)
                .HasMaxLength(15);
            builder.Property(x => x.Username)
                .IsRequired();
            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.Email)
                .IsRequired();
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.Telephone)
                .IsUnique();
            builder.Property(x => x.FirstName)
                .HasMaxLength(13);

            builder.HasMany(a => a.AuthorBooks)
                .WithOne(ab => ab.Author)
                .HasForeignKey(ab => ab.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(a => a.UseCaseAuthors)
                .WithOne(ab => ab.Author)
                .HasForeignKey(ab => ab.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
