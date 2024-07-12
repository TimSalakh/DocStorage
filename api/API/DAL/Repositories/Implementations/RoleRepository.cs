using API.DAL.Contexts;
using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Repositories.Implementations;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(DocStorageDbContext context)
        : base(context) { }

    public async Task<Role?> FindByNameAsync(string name)
    {
        return await _context.Role
            .FirstOrDefaultAsync(r => r.Name == name);
    }
}
