using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class PlaybackHistoryConfiguration : IEntityTypeConfiguration<PlaybackHistory>
    {
        public void Configure(EntityTypeBuilder<PlaybackHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.PlayedAt)
                .IsRequired()
                .HasConversion<DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => v.Kind == DateTimeKind.Utc ? v : DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey("UserId")
                .IsRequired();

            builder.HasOne(x => x.Track)
                .WithMany()
                .HasForeignKey("TrackId")
                .IsRequired();

            builder.Property<Guid>("UserId");
            builder.Property<Guid>("TrackId");
        }
    }
}