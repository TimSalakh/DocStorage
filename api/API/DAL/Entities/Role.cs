namespace API.DAL.Entities;

public class Role
{ 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<User>? Users { get; set; } = null;
}
