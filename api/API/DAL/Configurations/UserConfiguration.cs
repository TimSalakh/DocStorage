using API.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(u => u.ConfirmationCodes)
            .WithOne(cc => cc.User)
            .HasForeignKey(cc => cc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Publications)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(u => u.ConfirmedPublications)
            .WithOne(p => p.Confirmator)
            .HasForeignKey(p => p.ConfirmatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(u => u.IsEmailConfirmed)
            .HasDefaultValue(false);

        builder
           .HasIndex(u => u.Email)
           .IsUnique();
    }
}
