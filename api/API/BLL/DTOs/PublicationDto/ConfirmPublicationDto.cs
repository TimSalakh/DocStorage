using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.PublicationDto;

public class ConfirmPublicationDto
{
    [Required]
    public Guid ConfirmatorId { get; set; }

    [Required]
    public Guid PublicationId { get; set; }
}
