using API.BLL.DTOs.UserDTOs;
using API.BLL.Mappers;
using API.BLL.Services.Interfaces;
using API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.BLL.Services.Implementations;

public class StudentAccountService : IStudentAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public StudentAccountService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<ToStoreDto?> RegisterAsStudentAsync(RegisterStudentDto registerStudentDto)
    {
        var studentToRegister = registerStudentDto.ToUserTable();
        var result = await _userManager.CreateAsync(studentToRegister);

        if (!result.Succeeded)
            return default;

        var student = await _userManager.FindByEmailAsync(registerStudentDto.Email);

        var studentClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Student"),
            new Claim(ClaimTypes.Email, registerStudentDto.Email)
        };

        var token = _tokenService.GenerateToken(studentClaims);

        return new ToStoreDto
        {
            Token = token,
            Id = student!.Id,
            Email = student.Email!
        };
    }

    public async Task<ToStoreDto?> LoginAsStudentAsync(LoginUserDto loginStudentDto)
    {
        var student = await _userManager.Users
           .FirstOrDefaultAsync(u => u.Email == loginStudentDto.Email);
        if (student == null)
            return default;

        var result = await _signInManager
            .CheckPasswordSignInAsync(student, loginStudentDto.Password, false);
        if (!result.Succeeded)
            return default;

        var studentClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Student"),
            new Claim(ClaimTypes.Email, loginStudentDto.Email)
        };

        var token = _tokenService.GenerateToken(studentClaims);

        return new ToStoreDto
        {
            Token = token,
            Id = student!.Id,
            Email = student.Email!
        };
    }
}
