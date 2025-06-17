using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Username)
                .IsRequired()
                .HasConversion(u => u.Value, str => new Username(str))
                .HasMaxLength(UsernameValidator.MAX_LENGTH);

            builder.HasMany(x => x.FavoriteTracks)
                .WithMany()
                .UsingEntity(j => j.ToTable("FavoriteTracks"));

            builder.HasMany(x => x.Playlists)
                .WithOne(p => p.Owner)
                .HasForeignKey("OwnerId");

            builder.Ignore("_favoriteTracks");
            builder.Ignore("_playlists");
        }
    }
}