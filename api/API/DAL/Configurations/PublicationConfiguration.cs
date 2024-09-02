using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.DAL.Configurations;

public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .HasOne(p => p.Author)
            .WithMany(u => u.Publications)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(p => p.Confirmator)
            .WithMany(u => u.ConfirmedPublications)
            .HasForeignKey(p => p.ConfirmatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(p => p.IsConfirmed)
            .HasDefaultValue(false);
    }
}
