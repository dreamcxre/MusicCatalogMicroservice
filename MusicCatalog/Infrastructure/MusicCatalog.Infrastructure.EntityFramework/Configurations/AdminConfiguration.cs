using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.ValueObjects.Validators;

namespace MusicCatalog.Infrastructure.EntityFramework.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Username)
                .IsRequired()
                .HasConversion(u => u.Value, str => new Username(str))
                .HasMaxLength(UsernameValidator.MAX_LENGTH);

            builder.HasMany(x => x.AddedTracks)
                .WithOne()
                .HasForeignKey("AdminId")
                .IsRequired(false); 

            builder.Property<Guid?>("AdminId");

            builder.Ignore("_addedTracks");
        }
    }
}