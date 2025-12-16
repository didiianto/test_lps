using Microsoft.EntityFrameworkCore;
using Perpustakaan.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Perpustakaan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MasterJenisBuku> MasterJenisBuku { get; set; }
        public DbSet<Buku> Buku { get; set; }
        public DbSet<HistoryBuku> HistoryBuku { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buku>()
                .HasOne(b => b.JenisBuku)
                .WithMany(j => j.Bukus)
                .HasForeignKey(b => b.JenisBukuId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
