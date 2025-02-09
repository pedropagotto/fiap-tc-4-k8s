using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Configurations;
[ExcludeFromCodeCoverage]
public class AuthenticationConfigurations: IEntityTypeConfiguration<Authentication>
{
    public void Configure(EntityTypeBuilder<Authentication> builder)
    {
        builder.HasKey(u=> u.Id);
        
        var eProfileConverter = new ValueConverter<EProfile, string>(
            v => v.ToString(),
            v => (EProfile)Enum.Parse(typeof(EProfile), v));
        
        builder
            .Property(b => b.Profile)
            .IsRequired()
            .HasConversion(eProfileConverter);

        builder.Property(e => e.Email)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IDX_Authentication_Email")
            .IsUnique();
        
        builder.Property(e => e.EmailValidated)
            .HasMaxLength(5)
            .HasDefaultValue(false)
            .IsRequired();
        
        builder.Property(e => e.ChangePassword)
            .HasMaxLength(5)
            .HasDefaultValue(false)
            .IsRequired();
        
        builder.Property(e => e.Password)
            .IsRequired();
        
        builder.Property(e => e.CreatedAt)
            .HasDefaultValue(DateTime.Now)
            .IsRequired();
    }
}