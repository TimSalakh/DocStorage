using API.DAL.Entities;

namespace API.DAL.Repositories.Interfaces;

public interface IConfirmationCodeRepository : IBaseRepository<ConfirmationCode>
{
    Task<ConfirmationCode?> FindLastUserCodeAsync(User user);
}
