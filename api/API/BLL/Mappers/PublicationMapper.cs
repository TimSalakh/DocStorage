using API.BLL.DTOs.PublicationDto;
using API.DAL.Entities;

namespace API.BLL.Mappers;

public static class PublicationMapper
{
    public static Publication ToPublicationTable(this CreatePublicationDto createDto)
    {
        return new Publication
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            PublicationType = createDto.PublicationType,
            Description = createDto.Description,
            AuthorId = createDto.AuthorId,
            CreationTime = DateTime.UtcNow.AddHours(7)
        };
    }
}
