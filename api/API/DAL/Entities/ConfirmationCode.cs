namespace API.DAL.Entities;

public class ConfirmationCode
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ExpirationTime { get; set; }
    public DateTime NextResendTime { get; set; }
    public int Code { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}
