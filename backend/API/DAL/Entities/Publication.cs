namespace API.DAL.Entities;

public class Publication
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }
    public User? Author { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public Guid ConfirmatorId { get; set; }
    public User? Confirmator { get; set; }
    public DateTime ConfirmationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public ICollection<Document>? Documents { get; set; }
}
