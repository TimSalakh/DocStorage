using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.UserDTOs;

public class RegisterStudentDto : RegisterUserDto
{
    [Required(ErrorMessage = "Не может быть пустым.")]
    public string GroupTag { get; set; } = null!;
}
