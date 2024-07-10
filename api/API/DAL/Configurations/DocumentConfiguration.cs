using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.DAL.Entities;

namespace API.DAL.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(d => d.Id);

        builder
            .HasOne(d => d.Publication)
            .WithMany(p => p.Documents)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
