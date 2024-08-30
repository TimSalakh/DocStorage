using API.DAL.Entities;

namespace API.DAL.Repositories.Interfaces;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> FindByNameAsync(string name);
}
