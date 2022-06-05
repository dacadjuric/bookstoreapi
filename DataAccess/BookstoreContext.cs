using Domain;
using DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class BookstoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-F3CT45I\SQLEXPRESS01;Initial Catalog=BKSTR;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());

            modelBuilder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Genre>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Image>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Publisher>().HasQueryFilter(a => !a.IsDeleted);
            
            modelBuilder.Entity<BookAuthor>().HasKey(a => new { a.BookId, a.AuthorId});
            modelBuilder.Entity<BookGenre>().HasKey(a => new { a.BookId, a.GenreId});
            modelBuilder.Entity<Image>().HasKey(a => new { a.BookId});

        }

        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.ModifiedAt = null;
                            e.DeletedAt = null;
                            break;

                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.Now;
                            break;

                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }


    }
}
