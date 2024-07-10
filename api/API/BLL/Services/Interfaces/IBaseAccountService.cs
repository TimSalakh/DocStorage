using API.BLL.Common;
using API.BLL.DTOs.AccountDTOs;

namespace API.BLL.Services.Interfaces;

public interface IBaseAccountService
{
    public Task<ServiceResult<DataToStoreDto>> RegisterAsync(RegisterDto registerDto);
    public Task<ServiceResult<DataToStoreDto>> LoginAsync(LoginDto loginDto);
}
