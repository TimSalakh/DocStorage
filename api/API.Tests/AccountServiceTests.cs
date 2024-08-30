using API.BLL.Common;
using API.BLL.DTOs.AccountDTOs;
using API.BLL.Services.Implementations;
using API.BLL.Services.Interfaces;
using API.Controllers;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace API.Tests;

public class AccountServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IRoleRepository> _mockRoleRepository;
    private readonly Mock<ILogger<AccountController>> _mockLogger;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly Mock<ITwoStepAuthService> _mockTwoStepAuthService;
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockRoleRepository = new Mock<IRoleRepository>();
        _mockLogger = new Mock<ILogger<AccountController>>(); 
        _mockTokenService = new Mock<ITokenService>();
        _mockTwoStepAuthService = new Mock<ITwoStepAuthService>();

        _accountService = new AccountService(
            _mockUserRepository.Object,
            _mockRoleRepository.Object,
            _mockTokenService.Object,
            _mockLogger.Object,
            _mockTwoStepAuthService.Object);
    }

    [Fact]
    public async Task UserRegistration_ShouldBeSuccess_UserDoesntExist()
    {
        //Arrange
        var registerDto = new RegisterDto { Email = "test@example.com", IsTeacher = false };
        User? existingUser = null;
        var role = new Role { Name = "Student" };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(registerDto.Email))
            .ReturnsAsync(existingUser);
        _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        _mockRoleRepository.Setup(repo => repo.FindByNameAsync("Student"))
            .ReturnsAsync(role);
        _mockUserRepository.Setup(repo => repo.SetRoleAsync(It.IsAny<User>(), role))
            .Returns(Task.CompletedTask);
        _mockTwoStepAuthService.Setup(service => service.SendConfirmationCodeAsync(It.IsAny<User>()))
            .ReturnsAsync(ServiceResult<string>.Success(""));

        //Act
        var result = await _accountService.RegisterAsync(registerDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task UserRegistration_ShouldBeSuccess_UserExistButEmailIsUnconfirmed()
    {
        //Arrange
        var registerDto = new RegisterDto { Email = "test@example.com", IsTeacher = false };
        var existingUser = new User { Email = "test@example.com", IsEmailConfirmed = false }; 
        var role = new Role { Name = "Student" };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(registerDto.Email))
            .ReturnsAsync(existingUser);
        _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        _mockRoleRepository.Setup(repo => repo.FindByNameAsync("Student"))
            .ReturnsAsync(role);
        _mockUserRepository.Setup(repo => repo.SetRoleAsync(It.IsAny<User>(), role))
            .Returns(Task.CompletedTask);
        _mockTwoStepAuthService.Setup(service => service.SendConfirmationCodeAsync(It.IsAny<User>()))
            .ReturnsAsync(ServiceResult<string>.Success(""));

        //Act
        var result = await _accountService.RegisterAsync(registerDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task UserRegistration_ShouldBeFailure_UserExistAndEmailIsConfirmed()
    {
        //Arrange
        var registerDto = new RegisterDto { Email = "test@example.com", IsTeacher = false };
        var existingUser = new User { Email = "test@example.com", IsEmailConfirmed = true };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(registerDto.Email))
            .ReturnsAsync(existingUser);

        //Act
        var result = await _accountService.RegisterAsync(registerDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task UserLogin_ShouldBeSuccess_UserExist()
    {
        //Arrange
        var loginDto = new LoginDto { Email = "test@example.com" };
        var existingUser = new User { Email = "test@example.com", IsEmailConfirmed = true };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(existingUser);
        _mockTwoStepAuthService.Setup(service => service.SendConfirmationCodeAsync(It.IsAny<User>()))
            .ReturnsAsync(ServiceResult<string>.Success(""));

        //Act
        var result = await _accountService.LoginAsync(loginDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task UserLogin_ShouldBeFailure_UserDoesntExist()
    {
        //Arrange
        var loginDto = new LoginDto { Email = "test@example.com" };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(value: null);

        //Act
        var result = await _accountService.LoginAsync(loginDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task UserLogin_ShouldBeFailure_UserExistButEmailIsUnconfirmed()
    {
        //Arrange
        var loginDto = new LoginDto { Email = "test@example.com" };
        var existingUser = new User { Email = "test@example.com", IsEmailConfirmed = false };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(loginDto.Email))
            .ReturnsAsync(existingUser);

        //Act
        var result = await _accountService.LoginAsync(loginDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task EmailConfirmation_ShouldBeSuccess_CodesMatch()
    {
        //Arrange
        var confirmEmailDto = new ConfirmEmailDto { Email = "test@example.com", Code = 1234 };
        var existingUser = new User
        {
            Email = "test@example.com",
            IsEmailConfirmed = false,
            Role = new Role { Name = "User" }, 
        };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(confirmEmailDto.Email))
            .ReturnsAsync(existingUser);
        _mockTwoStepAuthService.Setup(repo => repo.MatchCodesAsync(It.IsAny<User>(), 1234))
            .ReturnsAsync(ServiceResult<string>.Success(""));
        _mockUserRepository.Setup(repo => repo.ConfirmEmailAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        _mockTokenService.Setup(service => service.GenerateToken(It.IsAny<IEnumerable<Claim>>()))
        .Returns("mocked-jwt-token");

        // Act
        var result = await _accountService.ConfirmEmailAsync(confirmEmailDto);

        //Assert
        Assert.True(result.Result);
        Assert.NotNull(result.Data);
        Assert.Equal("mocked-jwt-token", result.Data.Token);
        Assert.Equal(existingUser.Email, result.Data.Email);
    }

    [Fact]
    public async Task EmailConfirmation_ShouldBeFailure_CodesDoentMatch()
    {
        //Arrange
        var confirmEmailDto = new ConfirmEmailDto { Email = "test@example.com", Code = 1234 };
        var existingUser = new User
        {
            Email = "test@example.com",
            IsEmailConfirmed = false,
            Role = new Role { Name = "User" },
        };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(confirmEmailDto.Email))
            .ReturnsAsync(existingUser);
        _mockTwoStepAuthService.Setup(repo => repo.MatchCodesAsync(It.IsAny<User>(), 1234))
            .ReturnsAsync(ServiceResult<string>.Failure(""));

        // Act
        var result = await _accountService.ConfirmEmailAsync(confirmEmailDto);

        //Assert
        Assert.True(result.Result);
    }

    [Fact]
    public async Task EmailConfirmation_ShouldBeFailure_UserDoesntExist()
    {
        //Arrange
        var confirmEmailDto = new ConfirmEmailDto { Email = "test@example.com", Code = 1234 };

        _mockUserRepository.Setup(repo => repo.FindByEmailAsync(confirmEmailDto.Email))
            .ReturnsAsync(value: null);

        // Act
        var result = await _accountService.ConfirmEmailAsync(confirmEmailDto);

        //Assert
        Assert.True(result.Result);
    }
}
