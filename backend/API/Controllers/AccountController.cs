using API.BLL.DTOs.UserDTOs;
using API.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IStudentAccountService _studentAccountService;
    private readonly ITeacherAccountService _teacherAccountService;

    public AccountController(
        IStudentAccountService studentAccountService,
        ITeacherAccountService teacherAccountService)
    {
        _studentAccountService = studentAccountService;
        _teacherAccountService = teacherAccountService;
    }

    [HttpPost("register-as-student")]
    public async Task<IActionResult> RegisterAsStudent([FromBody] RegisterStudentDto registerStudentDto)
    {
        var result = await _studentAccountService.RegisterAsStudentAsync(registerStudentDto);
        return result != null ? Ok(result) : BadRequest("Пользователь с такими данными уже существует.");
    }

    [HttpPost("login-as-student")]
    public async Task<IActionResult> LoginAsStudent([FromBody] LoginUserDto LoginUserDto)
    {
        var result = await _studentAccountService.LoginAsStudentAsync(LoginUserDto);
        return result != null ? Ok(result) : Unauthorized("Неверная почта/пароль.");
    }

    [HttpPost("register-as-teacher")]
    public async Task<IActionResult> RegisterAsTeacher([FromBody] RegisterUserDto registerUsertDto)
    {
        var result = await _teacherAccountService.RegisterAsTeacherAsync(registerUsertDto);
        return result != null ? Ok(result) : BadRequest("Пользователь с такими данными уже существует.");
    }

    [HttpPost("login-as-teacher")]
    public async Task<IActionResult> LoginAsTeacher([FromBody] LoginUserDto LoginUserDto)
    {
        var result = await _teacherAccountService.LoginAsTeacherAsync(LoginUserDto);
        return result != null ? Ok(result) : Unauthorized("Неверная почта/пароль.");
    }

    [Authorize(Roles = "Student")]
    [HttpGet("student-endpoint")]
    public IActionResult StudentEndpoint()
    {
        return Ok("This is a student endpoint.");
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet("teacher-endpoint")]
    public IActionResult TeacherEndpoint()
    {
        return Ok("This is a teacher endpoint.");
    }
}
