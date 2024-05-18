using API.BLL.DTOs.UserDTOs;

namespace API.BLL.Services.Interfaces;

public interface IStudentAccountService
{
    Task<ToStoreDto?> RegisterAsStudentAsync(RegisterStudentDto registerStudentDto);
    Task<ToStoreDto?> LoginAsStudentAsync(LoginUserDto loginUserDto);
}
