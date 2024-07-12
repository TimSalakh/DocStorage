using API.BLL.Services.Interfaces;

namespace API.BLL.Services.Implementations;

public class EmailComposerService
{
    private readonly IEmailService _emailService;
    private readonly TwoStepAuthService _twoStepAuthService;

    public EmailComposerService(
        IEmailService emailService,
        TwoStepAuthService twoStepAuthService)
    {
        _emailService = emailService;
        _twoStepAuthService = twoStepAuthService;
    }

    public async Task SendConfirmationCodeAsync(string destinationEmail)
    {
        var cc = await _twoStepAuthService.GenerateCodeAsync(destinationEmail);
        var subject = "Ваш код подтверждения регистрации";
        var body = $"Ваш код подтверждения: {cc.Code}";

        await _emailService.SendEmailAsync(destinationEmail, subject, body);
    }
}