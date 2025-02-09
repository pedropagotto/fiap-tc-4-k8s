using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configurations;
[ExcludeFromCodeCoverage]
public class UserConfigurations: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u=> u.Id);
        
        builder.Property(u => u.FirstName)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(e => e.Email)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(e => e.Cpf)
            .HasMaxLength(16)
            .IsRequired();
        
        builder.Property(e => e.CreatedAt)
            .HasDefaultValue(DateTime.Now)
            .IsRequired();
        
        builder.HasOne<Authentication>(u => u.Authentication)
            .WithOne()
            .HasForeignKey<User>(fk => fk.AuthenticationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}