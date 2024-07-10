namespace API.BLL.DTOs.AccountDTOs;

public class DataToStoreDto
{
    public string Token { get; set; } = null!;
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
}
