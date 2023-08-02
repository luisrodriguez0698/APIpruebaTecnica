using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIprueba.DB;

public partial class dbConexion : DbContext
{
    public dbConexion()
    {
    }

    public dbConexion(DbContextOptions<dbConexion> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estado { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DataBase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
