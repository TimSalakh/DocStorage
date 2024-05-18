using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.UserDTOs;

public class LoginUserDto
{
    [Required(ErrorMessage = "Не может быть пустым.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Ошибка! Допустимая длина почты: 10-40")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Не может быть пустым.")]
    [StringLength(60, MinimumLength = 12, ErrorMessage = "Ошибка! Допустимая длина пароля: 12-60")]
    public string Password { get; set; } = null!;
}
