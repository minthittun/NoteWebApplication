using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NoteWebApplication.Models
{
    public partial class NoteWebDbContext : DbContext
    {
        public NoteWebDbContext()
        {
        }

        public NoteWebDbContext(DbContextOptions<NoteWebDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notes> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-E410567\\MTTMBPSQLSVR;Initial Catalog=NoteWebDb;Persist Security Info=True;User ID=sa;Password=a");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasKey(e => e.NoteId);

                entity.Property(e => e.NoteId).ValueGeneratedNever();

                entity.Property(e => e.NotedDate).HasColumnType("datetime");
            });
        }
    }
}
