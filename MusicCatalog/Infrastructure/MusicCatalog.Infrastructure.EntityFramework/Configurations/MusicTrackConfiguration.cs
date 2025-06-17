using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class MusicTrackConfiguration : IEntityTypeConfiguration<MusicTrack>
    {
        public void Configure(EntityTypeBuilder<MusicTrack> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasConversion(title => title.Value, str => new Title(str))
                .HasMaxLength(TitleValidator.MAX_LENGTH);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasConversion(description => description.Value, str => new Description(str));

            builder.Property(x => x.Duration)
                .IsRequired()
                .HasConversion(d => d.Ticks, t => TimeSpan.FromTicks(t));

            builder.Property(x => x.ReleaseDate)
                .IsRequired()
                .HasConversion<DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Property(x => x.Genre)
                .IsRequired()
                .HasConversion(genre => genre.Value, str => new Genre(str));

     
            builder.HasOne<Artist>(x => x.Artist)
                .WithMany(a => a.Tracks)
                .HasForeignKey("ArtistId")
                .IsRequired();

            builder.HasOne<Album>(x => x.Album)
                .WithMany(a => a.Tracks)
                .HasForeignKey("AlbumId")
                .IsRequired(false); 


            builder.Property<int>("Status");
            builder.Property<Guid>("ArtistId");
            builder.Property<Guid?>("AlbumId");
            builder.Property<Guid?>("AdminId");

            // Игнорируемые свойства
            builder.Ignore(x => x.IsAvailable);
            builder.Ignore(x => x.PlayCount);
        }
    }
}