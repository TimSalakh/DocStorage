using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.DAL.Configurations;

public class ConfirmationCodeConfiguration : IEntityTypeConfiguration<ConfirmationCode>
{
    public void Configure(EntityTypeBuilder<ConfirmationCode> builder)
    {
        builder.HasKey(cc => cc.Id);

        builder
            .HasOne(cc => cc.User)
            .WithMany(u => u.ConfirmationCodes)
            .HasForeignKey(cc => cc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
