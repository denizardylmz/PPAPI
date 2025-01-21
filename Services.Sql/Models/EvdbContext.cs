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

    public virtual DbSet<Notlar> Notlars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=evdb;Username=de_yilmaz;password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notlar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notlar_pkey");

            entity.ToTable("notlar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
