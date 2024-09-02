namespace API.DAL.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; } 
    public DateTime CreationTime { get; set; }
    public bool IsEmailConfirmed { get; set; } = false;
    public Guid? RoleId { get; set; }
    public Role? Role { get; set; }
    public ICollection<ConfirmationCode>? ConfirmationCodes { get; set; }
    public ICollection<Publication>? Publications { get; set; }
    public ICollection<Publication>? ConfirmedPublications { get; set; }
}
