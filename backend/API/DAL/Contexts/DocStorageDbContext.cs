using API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Contexts;

public class DocStorageDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<User> User { get; set; }

    public DocStorageDbContext(DbContextOptions<DocStorageDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //builder.ApplyConfiguration(new UserConfiguration());

        var roles = new IdentityRole[] 
        {
            new() { Name = "Guest", NormalizedName = "GUEST" },
            new() { Name = "Student", NormalizedName = "STUDENT" },
            new() { Name = "Teacher", NormalizedName = "TEACHER" },
            new() { Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
