using API.BLL.Common;
using API.DAL.Entities;

namespace API.BLL.Services.Interfaces;

public interface ITwoStepAuthService
{
    Task<ServiceResult<ConfirmationCode>> GenerateCodeAsync(User user);
    Task<ServiceResult<string>> SendConfirmationCodeAsync(User user);
    Task<ServiceResult<string>> MatchCodesAsync(User user, int code);
}
