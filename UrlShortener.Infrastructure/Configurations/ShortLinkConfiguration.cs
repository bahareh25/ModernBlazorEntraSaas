using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Configurations
{
    public class ShortLinkConfiguration : IEntityTypeConfiguration<ShortLink>
    {
        public void Configure(EntityTypeBuilder<ShortLink> builder)
    {
        builder.HasKey(x => x.Id);

        // URL being shortened
        builder.Property(x => x.OriginalUrl)
            .IsRequired()
            .HasMaxLength(2048);

        // Short code used in redirect
        builder.Property(x => x.ShortCode)
            .IsRequired()
            .HasMaxLength(10);

        // Creation timestamp
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // Optional expiration
        builder.Property(x => x.ExpirationDate)
            .IsRequired(false);

        // Optional password protection (hashed)
        builder.Property(x => x.PasswordHash)
            .HasMaxLength(255);

        // Foreign key
        builder.Property(x => x.UserId)
            .IsRequired();

        // Relationship: User → ShortLinks
        builder.HasOne(x => x.User)
            .WithMany(x => x.Links)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
   

            // Relationship: ShortLink → Visits
            builder.HasMany(x => x.Visits)
            .WithOne(x => x.ShortLink)
            .HasForeignKey(x => x.ShortLinkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("ShortLinks");

    }
}
}
