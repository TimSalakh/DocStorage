using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.AccountDTOs;

public class RegisterDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public bool IsTeacher { get; set; }
}
