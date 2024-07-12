namespace API.DAL.Entities;

public class ConfirmationCode
{
    public Guid Id { get; set; }
    public string DestinationEmail { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime ExpirationTime { get; set; }
    public int Code { get; set; }
}
