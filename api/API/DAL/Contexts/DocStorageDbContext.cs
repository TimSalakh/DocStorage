using API.DAL.Configurations;
using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Contexts;

public class DocStorageDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<ConfirmationCode> ConfirmationCode { get; set; }
    public DbSet<Publication> Publication { get; set; }


    public DocStorageDbContext(DbContextOptions<DocStorageDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new ConfirmationCodeConfiguration());
        builder.ApplyConfiguration(new PublicationConfiguration());

        var roles = new List<Role>()
        {
            new Role { Id = Guid.NewGuid(), Name = "Student" },
            new Role { Id = Guid.NewGuid(), Name = "Teacher" },
            new Role { Id = Guid.NewGuid(), Name = "Admin" }
        };

        builder.Entity<Role>().HasData(roles);
    }
}
