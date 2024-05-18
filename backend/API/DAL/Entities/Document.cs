namespace API.DAL.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid PublicationId { get; set; }
    public Publication? Publication { get; set; }
}
