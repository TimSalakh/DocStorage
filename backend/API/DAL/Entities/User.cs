using Microsoft.AspNetCore.Identity;

namespace API.DAL.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string? Bio { get; set; }
    public DateTime CreationDate { get; set; }
}
