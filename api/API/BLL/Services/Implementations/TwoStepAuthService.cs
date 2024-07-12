using API.BLL.Common;
using API.Controllers;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;

namespace API.BLL.Services.Implementations;

public class TwoStepAuthService
{
    private readonly IBaseRepository<ConfirmationCode> _baseRepository;
    private readonly ILogger<AccountController> _logger;

    public TwoStepAuthService(
        IBaseRepository<ConfirmationCode> baseRepository,
        ILogger<AccountController> logger)
    {
        _baseRepository = baseRepository;
        _logger = logger;
    }

    public async Task<ConfirmationCode> GenerateCodeAsync(string destinationEmail)
    {
        var cc = new ConfirmationCode
        {
            Id = Guid.NewGuid(),
            DestinationEmail = destinationEmail,
            CreationTime = DateTime.UtcNow.AddHours(7),
            ExpirationTime = DateTime.UtcNow.AddHours(7).AddMinutes(5),
            Code = new Random().Next(1000, 9999)
        };

        _logger.LogInformation("Создан код подтверждения {code} для {email}.", cc.Code, cc.DestinationEmail);
        await _baseRepository.AddAsync(cc);

        return cc;
    }

    public async Task<ServiceResult<string>> MatchCodesAsync(string userEmail, int userCode)
    {
        _logger.LogInformation("Получен код подтверждения {code} для {email}.", userCode, userEmail);

        var codes = await _baseRepository.GetAllAsync();

        var userLastCc = codes
            .Where(cc => cc.DestinationEmail == userEmail)
            .OrderByDescending(cc => cc.CreationTime)
            .First();

        _logger.LogInformation("Найден последний код подтверждения {code} для {email}.", userLastCc.Code, userEmail);

        if (DateTime.Now > userLastCc.ExpirationTime)
            return ServiceResult<string>.Failure("Время действия кода подтверждения истекло.");

        if (userLastCc.Code != userCode)
            return ServiceResult<string>.Failure("Неверный код подтверждения.");

        return ServiceResult<string>.Success(string.Empty);
    }
}
