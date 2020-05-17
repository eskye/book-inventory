using System;
using BookInventory.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.DataLayer
{
    public class BookInventoryDbContext : DbContext
    {
        public BookInventoryDbContext(DbContextOptions<BookInventoryDbContext> contextOptions)
            : base(contextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(p => p.Id);

                b.Property(t => t.Title).HasColumnType("varchar(200)").IsRequired();
                b.Property(t => t.Author).HasColumnType("varchar(100)").IsRequired();
                b.Property(t => t.Isbn).HasColumnType("varchar(100)").IsRequired();
                b.Property(t => t.Publisher).HasColumnType("varchar(200)").IsRequired();
                b.Property(t => t.Year).HasColumnType("varchar(10)").IsRequired();
            });
        }

        public DbSet<Book> Books { get; set; }
    }
}
