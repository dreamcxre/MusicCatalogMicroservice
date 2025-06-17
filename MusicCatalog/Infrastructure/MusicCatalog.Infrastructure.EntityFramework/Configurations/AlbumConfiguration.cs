using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasConversion(title => title.Value, str => new Title(str))
                .HasMaxLength(TitleValidator.MAX_LENGTH);

            builder.Property(x => x.ReleaseDate)
                .IsRequired()
                .HasConversion<DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.HasOne(x => x.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey("ArtistId")
                .IsRequired();

            builder.HasMany(x => x.Tracks)
                .WithOne(t => t.Album)
                .HasForeignKey("AlbumId")
                .IsRequired(false);

            builder.Ignore("_tracks");
        }
    }
}