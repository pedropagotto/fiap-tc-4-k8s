using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;
[ExcludeFromCodeCoverage]
public class ContactConfigurations: IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(u=> u.Id);
        builder.Property(a => a.Ddd).HasMaxLength(3);
        builder.Property(a => a.Phone).HasMaxLength(10);
        builder.Property(a => a.Email).HasMaxLength(200);
    }
}