using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Username)
                .IsRequired()
                .HasConversion(u => u.Value, str => new Username(str))
                .HasMaxLength(UsernameValidator.MAX_LENGTH);

            builder.HasMany(x => x.Tracks)
                .WithOne(x => x.Artist)
                .HasForeignKey("ArtistId");

            builder.HasMany(x => x.Albums)
                .WithOne(a => a.Artist)
                .HasForeignKey("ArtistId");

            builder.Ignore("_tracks");
            builder.Ignore("_albums");
        }
    }
}