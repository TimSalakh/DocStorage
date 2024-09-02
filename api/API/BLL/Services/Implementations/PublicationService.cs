using API.BLL.Common;
using API.BLL.DTOs.PublicationDto;
using API.BLL.Mappers;
using API.Controllers;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;

namespace API.BLL.Services.Implementations;

public class PublicationService
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<PublicationController> _logger;

    public PublicationService(
        IPublicationRepository publicationRepository, 
        IUserRepository userRepository,
        ILogger<PublicationController> logger)
    {
        _publicationRepository = publicationRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ServiceResult<string>> CreatePublicationAsync(CreatePublicationDto createDto)
    {
        var newPublication = createDto.ToPublicationTable();
        await _publicationRepository.AddAsync(newPublication);

        return ServiceResult<string>.Success("");
    }

    public async Task<ServiceResult<string>> ConfirmPublicationAsync(ConfirmPublicationDto confirmDto)
    {
        var publication = await _publicationRepository.FindByIdAsync(confirmDto.PublicationId);
        var confirmator = await _userRepository.FindByIdAsync(confirmDto.ConfirmatorId);

        if (publication == null || confirmator == null)
            return LogAndReturnError<string>("Не найден пользователь или публикация.", 
                "Произошла ошибка при подтверждении побликации.");

        await _publicationRepository.ConfirmPublicationAsync(publication, confirmator);
        return ServiceResult<string>.Success("");
    }

    public async Task<ServiceResult<IEnumerable<Publication>?>> GetUsersPublicationsAsync(Guid userId, SortOption sortOption)
    {
        var totalPublication = await _publicationRepository.GetAllAsync();
        var usersPublication = totalPublication
            .Where(p => p.AuthorId == userId)
            .OrderByDescending(p => p.CreationTime);

        IEnumerable<Publication>? sortedPublications = null;

        sortedPublications = sortOption switch
        {
            SortOption.ConfirmedOnly => usersPublication.Where(p => p.IsConfirmed),
            SortOption.UnconfirmedOnly => usersPublication.Where(p => !p.IsConfirmed),
            SortOption.Default => usersPublication,
            _ => usersPublication,
        };

        return ServiceResult<IEnumerable<Publication>?>.Success(sortedPublications);
    }

    private ServiceResult<T> LogAndReturnError<T>(string logMessage, string returnMessage)
    {
        _logger.LogError(logMessage);
        return ServiceResult<T>.Failure(returnMessage);
    }
}

public enum SortOption
{
    Default,
    ConfirmedOnly,
    UnconfirmedOnly
}
