using Book_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_API.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                     .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Title)
                     .IsUnique();

                entity.Property(e => e.PublicationYear)
                      .IsRequired();

                entity.Property(e => e.AuthorName)
                      .IsRequired();

                entity.Property(e => e.ViewsCount)
                      .HasDefaultValue(0);


                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);
            });
        }

        public DbSet<Book> Books { get; set; } = null!;
    }
}
