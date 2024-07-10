using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.PublicationDto;

public class CreatePublicationDto
{
    [Required(ErrorMessage = "Не может быть пустым.")]
    public Guid AuthorId { get; set; }

    [Required(ErrorMessage = "Не может быть пустым.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Не может быть пустым.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Должен содержать хотя бы один документ.")]
    public IEnumerable<IFormFile> Documents { get; set; } = null!;
}