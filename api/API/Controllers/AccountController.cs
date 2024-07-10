using API.BLL.DTOs.AccountDTOs;
using API.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IBaseAccountService _baseAccountService;

    public AccountController(IBaseAccountService baseAccountService)
    {
        _baseAccountService = baseAccountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsStudent([FromBody] RegisterDto registerDto)
    {
        var registrationResult = await _baseAccountService.RegisterAsync(registerDto);
        return registrationResult.Result ? 
            Ok(registrationResult.Data) : 
            BadRequest(new { Error = registrationResult.ErrorMessage });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto LoginDto)
    {
        var loginResult = await _baseAccountService.LoginAsync(LoginDto);
        return loginResult.Result ? 
            Ok(loginResult.Data) : 
            Unauthorized(new { Error = loginResult.ErrorMessage });
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

    [HttpGet("dummy-endpoint")]
    public IActionResult DummyEndpoint()
    {
        return Ok("This is a DUMMY endpoint.");
    }
}
 