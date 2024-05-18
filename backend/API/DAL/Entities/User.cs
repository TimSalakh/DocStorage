using Microsoft.AspNetCore.Identity;

namespace API.DAL.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string? GroupTag { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<Publication>? Publications { get; set; }
    public ICollection<Publication>? Confirmations { get; set; } 
}
