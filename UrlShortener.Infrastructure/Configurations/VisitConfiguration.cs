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
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.ShortLinkId)
                .IsRequired();

            builder.Property(x => x.VisitedAt)
                .IsRequired();

            builder.Property(x => x.IpAddress)
                .HasMaxLength(45); // supports IPv4 + IPv6

            builder.Property(x => x.UserAgent)
                .HasMaxLength(500); // reasonable limit for headers

            builder.ToTable("Visits");

           
        }
    }
}
