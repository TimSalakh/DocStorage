using API.BLL.DTOs.UserDTOs;

namespace API.BLL.Services.Interfaces;

public interface ITeacherAccountService
{
    Task<ToStoreDto?> RegisterAsTeacherAsync(RegisterUserDto registerUserDto);
    Task<ToStoreDto?> LoginAsTeacherAsync(LoginUserDto loginUserDto);
}
