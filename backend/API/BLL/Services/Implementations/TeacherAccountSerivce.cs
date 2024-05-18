using API.BLL.DTOs.UserDTOs;
using API.BLL.Mappers;
using API.BLL.Services.Interfaces;
using API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.BLL.Services.Implementations;

public class TeacherAccountSerivce : ITeacherAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public TeacherAccountSerivce(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<ToStoreDto?> RegisterAsTeacherAsync(RegisterUserDto registerUserDto)
    {
        var teacherToRegister = registerUserDto.ToUserTable();
        var result = await _userManager.CreateAsync(teacherToRegister);

        if (!result.Succeeded)
            return default;

        var teacher = await _userManager.FindByEmailAsync(registerUserDto.Email);

        var teacherClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Teacher"),
            new Claim(ClaimTypes.Email, registerUserDto.Email)
        };

        var token = _tokenService.GenerateToken(teacherClaims);

        return new ToStoreDto
        {
            Token = token,
            Id = teacher!.Id,
            Email = teacher.Email!
        };
    }

    public async Task<ToStoreDto?> LoginAsTeacherAsync(LoginUserDto loginUserDto)
    {
        var teacher = await _userManager.Users
           .FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);
        if (teacher == null)
            return default;

        var result = await _signInManager
            .CheckPasswordSignInAsync(teacher, loginUserDto.Password, false);
        if (!result.Succeeded)
            return default;

        var teacherClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Student"),
            new Claim(ClaimTypes.Email, loginUserDto.Email)
        };

        var token = _tokenService.GenerateToken(teacherClaims);

        return new ToStoreDto
        {
            Token = token,
            Id = teacher!.Id,
            Email = teacher.Email!
        };
    }
}
