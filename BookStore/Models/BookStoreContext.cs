using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Authors_pk");

            entity.ToTable("Authors", "Bookstore");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bio)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC27AF603801");

            entity.ToTable("Books", "Bookstore");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.Image)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Summary)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Author).WithMany(p => p.InverseAuthor)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("Books_Books_ID_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
