using API.BLL.Common;
using API.BLL.DTOs.AccountDTOs;
using API.BLL.Mappers;
using API.BLL.Services.Interfaces;
using API.Controllers;
using API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.BLL.Services.Implementations;

public class BaseAccountService : IBaseAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;
    private readonly ITokenService _tokenService;

    public BaseAccountService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<ServiceResult<DataToStoreDto>> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return ServiceResult<DataToStoreDto>.Failure("Пользователь с таким почтовым адресом уже существует.");

        var userToRegister = registerDto.ToUserTable();
        var userCreationResult = await _userManager.CreateAsync(userToRegister, registerDto.Password);
        if (!userCreationResult.Succeeded)
            return LogAndReturnError<DataToStoreDto>(userCreationResult.Errors, "Ошибка при регистрации пользователя.");

        var role = registerDto.IsTeacher ? "Teacher" : "Student";
        var addingRoleResult = await _userManager.AddToRoleAsync(userToRegister, role);
        if (!addingRoleResult.Succeeded)
            return LogAndReturnError<DataToStoreDto>(addingRoleResult.Errors, "Ошибка при добавлении соответствующей роли.");

        var token = GetTokenWithClaims(userToRegister.Email!, role);
        var dataToStore = new DataToStoreDto 
        { 
            Token = token, 
            Id = userToRegister.Id, 
            Email = userToRegister.Email! 
        };

        return ServiceResult<DataToStoreDto>.Success(dataToStore);
    }

    public async Task<ServiceResult<DataToStoreDto>> LoginAsync(LoginDto loginDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(loginDto.Email);
        if (existingUser == null)
            return LogAndReturnError<DataToStoreDto>("Пользователь не найден.", "Неверная почта или пароль.");

        var passwordMatchingResult = await _signInManager.CheckPasswordSignInAsync(existingUser, loginDto.Password, false);
        if (!passwordMatchingResult.Succeeded)
            return LogAndReturnError<DataToStoreDto>("Пароли не совпадают.", "Неверная почта или пароль.");

        var roles = await _userManager.GetRolesAsync(existingUser);
        var token = GetTokenWithClaims(existingUser.Email!, roles.First());
        var dataToStore = new DataToStoreDto 
        { 
            Token = token, 
            Id = existingUser.Id, 
            Email = existingUser.Email! 
        };

        return ServiceResult<DataToStoreDto>.Success(dataToStore);
    }

    private string GetTokenWithClaims(string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Email, email)
        };
        return _tokenService.GenerateToken(claims);
    }

    private ServiceResult<T> LogAndReturnError<T>(IEnumerable<IdentityError> errors, string defaultMessage)
    {
        errors
            .ToList()
            .ForEach(e => _logger.LogError("Ошибка: {desc}", e.Description));

        return ServiceResult<T>.Failure(defaultMessage);
    }

    private ServiceResult<T> LogAndReturnError<T>(string logMessage, string returnMessage)
    {
        _logger.LogError(logMessage);
        return ServiceResult<T>.Failure(returnMessage);
    }
}
