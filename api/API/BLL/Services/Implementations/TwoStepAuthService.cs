using API.BLL.Common;
using API.BLL.Services.Interfaces;
using API.Controllers;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;

namespace API.BLL.Services.Implementations;

public class TwoStepAuthService
{
    private readonly IConfirmationCodeRepository _confirmationCodeRepoRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<AccountController> _logger;

    public TwoStepAuthService(
        IConfirmationCodeRepository confirmationCodeRepoRepository,
        IEmailService emailService,
        ILogger<AccountController> logger)
    {
        _confirmationCodeRepoRepository = confirmationCodeRepoRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<ServiceResult<ConfirmationCode>> GenerateCodeAsync(User user)
    {
        var userLastCc = await _confirmationCodeRepoRepository.FindLastUserCodeAsync(user);

        if (userLastCc != null && userLastCc.NextResendTime > DateTime.Now)
            return ServiceResult<ConfirmationCode>.Failure("Код не доступен для отправки. Пожалуйста, подождите.");

        var cc = new ConfirmationCode
        {
            Id = Guid.NewGuid(),
            CreationTime = DateTime.UtcNow.AddHours(7),
            ExpirationTime = DateTime.UtcNow.AddHours(7).AddMinutes(5),
            NextResendTime = DateTime.UtcNow.AddHours(7).AddMinutes(1),
            Code = new Random().Next(1000, 9999),
            User = user,
            UserId = user.Id
        };

        _logger.LogInformation("Создан код подтверждения {code} для {email}.", cc.Code, user.Email);
        await _confirmationCodeRepoRepository.AddAsync(cc);

        return ServiceResult<ConfirmationCode>.Success(cc);
    }

    public async Task<ServiceResult<string>> SendConfirmationCodeAsync(User user)
    {
        var generationResult = await GenerateCodeAsync(user);
        if (!generationResult.Result)
            return ServiceResult<string>.Failure(generationResult.ErrorMessage!);

        var subject = "Ваш код подтверждения";
        var body = $"Код подтверждения: {generationResult.Data!.Code}";

        await _emailService.SendEmailAsync(user.Email!, subject, body);

        return ServiceResult<string>.Success(string.Empty);
    }

    public async Task<ServiceResult<string>> MatchCodesAsync(User user, int code)
    {
        _logger.LogInformation("Получен код подтверждения {code} для {email}.", code, user.Email);

        var userLastCc = await _confirmationCodeRepoRepository.FindLastUserCodeAsync(user);

        if (userLastCc == null)
            return ServiceResult<string>.Failure("Код подтверждения не найден.");

        _logger.LogInformation("Найден последний код подтверждения {code} для {email}.", userLastCc!.Code, user.Email);

        if (DateTime.Now > userLastCc.ExpirationTime)
            return ServiceResult<string>.Failure("Время действия кода подтверждения истекло.");

        if (userLastCc.Code != code)
            return ServiceResult<string>.Failure("Неверный код подтверждения.");

        return ServiceResult<string>.Success(string.Empty);
    }
}
