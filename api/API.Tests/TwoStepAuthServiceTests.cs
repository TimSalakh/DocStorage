using API.BLL.Services.Implementations;
using API.BLL.Services.Interfaces;
using API.Controllers;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.Tests;

public class TwoStepAuthServiceTests
{
    private readonly Mock<IConfirmationCodeRepository> _mockConfirmationCodeRepo;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Mock<ILogger<AccountController>> _mockLogger;
    private readonly TwoStepAuthService _service;

    public TwoStepAuthServiceTests()
    {
        _mockConfirmationCodeRepo = new Mock<IConfirmationCodeRepository>();
        _mockEmailService = new Mock<IEmailService>();
        _mockLogger = new Mock<ILogger<AccountController>>();

        _service = new TwoStepAuthService(
            _mockConfirmationCodeRepo.Object, 
            _mockEmailService.Object, 
            _mockLogger.Object);
    }

    [Fact]
    public async Task CodeGeneration_ShouldBeSuccess_CodeResendAllowed()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
        var lastCode = new ConfirmationCode
        {
            Id = Guid.NewGuid(),
            NextResendTime = DateTime.UtcNow.AddHours(6).AddMinutes(55),
        };

        _mockConfirmationCodeRepo.Setup(repo => repo.FindLastUserCodeAsync(user))
            .ReturnsAsync(lastCode);

        // Act
        var result = await _service.GenerateCodeAsync(user);

        // Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task CodeGeneration_ShouldBeFailure_CodeResendNotAllowed()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
        var lastCode = new ConfirmationCode
        {
            Id = Guid.NewGuid(),
            NextResendTime = DateTime.UtcNow.AddHours(7).AddMinutes(1),
        };

        _mockConfirmationCodeRepo.Setup(repo => repo.FindLastUserCodeAsync(user))
            .ReturnsAsync(lastCode);

        // Act
        var result = await _service.GenerateCodeAsync(user);

        // Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task CodeMacthing_ShouldBeSuccess()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
        var lastCode = new ConfirmationCode
        {
            Id = Guid.NewGuid(),
            ExpirationTime = DateTime.UtcNow.AddHours(7).AddMinutes(5),
            Code = 1234
        };

        _mockConfirmationCodeRepo.Setup(repo => repo.FindLastUserCodeAsync(user))
            .ReturnsAsync(lastCode);

        // Act
        var result = await _service.MatchCodesAsync(user, 1234);

        // Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task CodeMacthing_ShouldBeFailure()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Email = "test@example.com" };
        var lastCode = new ConfirmationCode
        {
            Id = Guid.NewGuid(),
            ExpirationTime = DateTime.UtcNow.AddHours(7).AddMinutes(5),
            Code = 4321
        };

        _mockConfirmationCodeRepo.Setup(repo => repo.FindLastUserCodeAsync(user))
            .ReturnsAsync(lastCode);

        // Act
        var result = await _service.MatchCodesAsync(user, 1234);

        // Assert
        Assert.True(result.Result);
    }
}
