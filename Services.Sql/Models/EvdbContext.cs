using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Services.Sql.Models;

public partial class EvdbContext : DbContext
{
    public EvdbContext()
    {
    }

    public EvdbContext(DbContextOptions<EvdbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Note> Notes { get; set; }
    public virtual DbSet<NoteTag> NoteTags { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=evdb;Username=de_yilmaz;password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("notes");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.CreatedAt).HasDefaultValue(DateTime.UtcNow);
                entity.Property(x => x.UpdatedAt).HasDefaultValue(DateTime.UtcNow);
                
                entity.HasMany(x => x.NoteTags).WithOne().HasForeignKey(x => x.NoteId).HasPrincipalKey(x => x.Id);
                entity.HasOne<User>(x => x.User).WithMany().HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id).IsRequired();
            }
        );

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("tags");
            entity.HasKey(x => x.Id);
        });

        modelBuilder.Entity<NoteTag>(entity =>
        {
            entity.ToTable("notetags");
            entity.HasKey(x => new { x.NoteId, x.TagId });

            entity.HasOne<Tag>(x => x.Tag).WithOne()
                .HasForeignKey<NoteTag>(x => x.TagId)
                .HasPrincipalKey<Tag>(x => x.Id);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Username).IsUnique();
            entity.Property(x => x.Username).HasMaxLength(50);
            entity.Property(x => x.Username).IsRequired();
            
            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Surname).IsRequired();
            entity.Property(x => x.Password).IsRequired();
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
