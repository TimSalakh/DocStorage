using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.UserDTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Не может быть пустым.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Ошибка! Допустимая длина имени: 3-50")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Не может быть пустым.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Ошибка! Допустимая длина фамилии: 3-50")]
    public string Surname { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Ошибка! Допустимая длина отчества: 0-50")]
    public string? Patronymic { get; set; }

    [Required(ErrorMessage = "Не может быть пустым.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Ошибка! Допустимая длина почты: 10-40")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Не может быть пустым.")]
    [StringLength(60, MinimumLength = 12, ErrorMessage = "Ошибка! Допустимая длина пароля: 12-60")]
    public string Password { get; set; } = null!;
}
