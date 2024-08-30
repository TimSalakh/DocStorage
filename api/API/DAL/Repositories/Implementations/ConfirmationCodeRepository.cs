using API.DAL.Contexts;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Repositories.Implementations;

public class ConfirmationCodeRepository : BaseRepository<ConfirmationCode>, IConfirmationCodeRepository
{
    public ConfirmationCodeRepository(DocStorageDbContext context)
        : base(context) { }

    public async Task<ConfirmationCode?> FindLastUserCodeAsync(User user)
    {
        var codes = await GetAllAsync();

        var userLastCc = await codes
            .Where(cc => cc.UserId == user.Id)
            .OrderByDescending(cc => cc.CreationTime)
            .FirstOrDefaultAsync();

        return userLastCc;
    }
}
