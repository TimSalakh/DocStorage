using API.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.PublicationDto;

public class CreatePublicationDto
{
    [Required]
    public Guid AuthorId { get; set; }

    [Required]

    public string Name { get; set; } = null!;

    [Required]
    public PublicationType PublicationType { get; set; }

    [Required]
    public string Description { get; set; } = null!;
}