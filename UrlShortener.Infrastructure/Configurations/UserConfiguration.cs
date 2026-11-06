using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            // External identity provider reference (Entra ID)
            builder.Property(x => x.EntraObjectId)
                .HasMaxLength(100);
            // Email
            builder.Property(x => x.Email)
                .HasMaxLength(256);

            // Display name
            builder.Property(x => x.DisplayName)
                .HasMaxLength(200);

            //// Relationship: User → ShortLinks (1-to-many)
            //builder.HasMany(x => x.Links)
            //    .WithOne()
            //    .HasForeignKey(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Users");

        }
    }
}
