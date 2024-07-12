using API.BLL.DTOs.AccountDTOs;
using API.BLL.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly AccountService _ccountService;

    public AccountController(AccountService AccountService)
    {
        _ccountService = AccountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsStudent([FromBody] RegisterDto registerDto)
    {
        var registrationResult = await _ccountService.RegisterAsync(registerDto);
        return registrationResult.Result ?
            Ok(registrationResult.Data) :
            BadRequest(new { Error = registrationResult.ErrorMessage });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto LoginDto)
    {
        var loginResult = await _ccountService.LoginAsync(LoginDto);
        return loginResult.Result ?
            Ok(loginResult.Data) :
            BadRequest(new { Error = loginResult.ErrorMessage });
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
    {
        var emailConfirmationResult = await _ccountService.ConfirmEmailAsync(confirmEmailDto);
        return emailConfirmationResult.Result ?
            Ok(emailConfirmationResult.Data) :
            Unauthorized(new { Error = emailConfirmationResult.ErrorMessage });
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
 