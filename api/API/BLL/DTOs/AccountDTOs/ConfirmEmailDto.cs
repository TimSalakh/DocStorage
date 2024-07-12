using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.AccountDTOs;

public class ConfirmEmailDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public int Code { get; set; }
}
