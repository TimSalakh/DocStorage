using API.BLL.DTOs.PublicationDto;
using API.BLL.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/publication")]
public class PublicationController : ControllerBase
{
    private readonly PublicationService _publicationService;

    public PublicationController(PublicationService publicationService)
    {
        _publicationService = publicationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePublicationDto createDto)
    {
        var creationResult = await _publicationService.CreatePublicationAsync(createDto);
        return creationResult.Result ?
            Created() :
            BadRequest(new { Error = creationResult.ErrorMessage });
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmPublicationDto confirmDto)
    {
        var confirmationResult = await _publicationService.ConfirmPublicationAsync(confirmDto);
        return confirmationResult.Result ?
            Ok() :
            BadRequest(new { Error = confirmationResult.ErrorMessage });
    }

    [HttpGet("fetch/user-id={userId:guid}&sort-option={sortOption:int}")]
    public async Task<IActionResult> Fetch(Guid userId, int sortOption)
    {
        if (!Enum.IsDefined(typeof(SortOption), sortOption))
            return BadRequest(new { Error = "Invalid sort option value." });
        
        var parsedSortOption = (SortOption)sortOption;

        var fetchingResult = await _publicationService.GetUsersPublicationsAsync(userId, parsedSortOption);
        return fetchingResult.Result
            ? Ok(fetchingResult.Data)
            : BadRequest(new { Error = fetchingResult.ErrorMessage });
    }
}

