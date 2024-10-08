﻿using API.BLL.Common;
using API.BLL.DTOs.AccountDTOs;
using API.BLL.Mappers;
using API.BLL.Services.Interfaces;
using API.Controllers;
using API.DAL.Repositories.Implementations;
using API.DAL.Repositories.Interfaces;
using System.Security.Claims;

namespace API.BLL.Services.Implementations;

public class AccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<AccountController> _logger;
    private readonly ITokenService _tokenService;
    private readonly ITwoStepAuthService _twoStepAuthService;

    public AccountService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ITokenService tokenService,
        ILogger<AccountController> logger,
        ITwoStepAuthService twoStepAuthService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
        _logger = logger;
        _twoStepAuthService = twoStepAuthService;
    }

    public async Task<ServiceResult<string>> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userRepository.FindByEmailAsync(registerDto.Email);
        if (existingUser != null && existingUser.IsEmailConfirmed)
            return LogAndReturnError<string>("Такой пользователь уже существует.",
                "Пользователь с таким почтовым адресом уже существует.");

        var newUser = registerDto.ToUserTable();

        if (existingUser != null && !existingUser.IsEmailConfirmed)
            await _userRepository.DeleteAsync(existingUser.Email!);
        
        await _userRepository.AddAsync(newUser);
        var receivedRole = registerDto.IsTeacher ? "Teacher" : "Student";
        var role = await _roleRepository.FindByNameAsync(receivedRole);

        await _userRepository.SetRoleAsync(newUser, role!);
        var sendingResult = await _twoStepAuthService.SendConfirmationCodeAsync(newUser);

        if (!sendingResult.Result)
            return LogAndReturnError<string>("Проблема с кодом подтверждения.", sendingResult.ErrorMessage!);

        return ServiceResult<string>.Success("Код подтверждения отправлен по указанному адресу.");
    }

    public async Task<ServiceResult<string>> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.FindByEmailAsync(loginDto.Email);
        if (user == null || !user.IsEmailConfirmed)
            return LogAndReturnError<string>("Пользователь не найден или у него не подтверждена почта.", "Пользователь не найден.");

        var sendingResult = await _twoStepAuthService.SendConfirmationCodeAsync(user);

        if (!sendingResult.Result)
            return LogAndReturnError<string>("Проблема с кодом подтверждения.", sendingResult.ErrorMessage!);

        return ServiceResult<string>.Success("Код подтверждения отправлен по указанному адресу.");
    }

    public async Task<ServiceResult<DataToStoreDto>> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
    {
        var user = await _userRepository.FindByEmailAsync(confirmEmailDto.Email);
        if (user == null)
            return LogAndReturnError<DataToStoreDto>("Указан неверный почтовый адрес.", "Пользователь не найден.");

        var validationResult = await _twoStepAuthService.MatchCodesAsync(user, confirmEmailDto.Code);
        if (!validationResult.Result)
            return LogAndReturnError<DataToStoreDto>(validationResult.ErrorMessage!, validationResult.ErrorMessage!);

        await _userRepository.ConfirmEmailAsync(user);
        var token = GetTokenWithClaims(user.Email!, user.Role!.Name);

        var dataToStore = new DataToStoreDto
        {
            Token = token,
            Id = user.Id,
            Email = user.Email!
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

    private ServiceResult<T> LogAndReturnError<T>(string logMessage, string returnMessage)
    {
        _logger.LogError(logMessage);
        return ServiceResult<T>.Failure(returnMessage);
    }
}
