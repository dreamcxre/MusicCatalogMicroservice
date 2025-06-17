using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(TitleValidator.MAX_LENGTH);

            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey("OwnerId")
                .IsRequired();

            builder.HasMany(x => x.Tracks)
                .WithMany()
                .UsingEntity(j => j.ToTable("PlaylistTracks"));

            builder.Ignore("_tracks");
        }
    }
}