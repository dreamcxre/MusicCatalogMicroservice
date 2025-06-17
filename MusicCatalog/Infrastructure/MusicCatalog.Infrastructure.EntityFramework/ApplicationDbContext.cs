using Microsoft.EntityFrameworkCore;
using MusicCatalog.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MusicCatalog.Infrastructure.EntityFramework
{
    public class MusicCatalogDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<MusicTrack> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaybackHistory> PlaybackHistories { get; set; }

        public MusicCatalogDbContext(DbContextOptions<MusicCatalogDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Включает логирование чувствительных данных (для разработки)
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Автоматически применяет все IEntityTypeConfiguration из текущей сборки
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}