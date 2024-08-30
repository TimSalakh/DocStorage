using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    Task ConfirmEmailAsync(User user);
    Task SetRoleAsync(User user, Role role);
    Task DeleteAsync(string email);
}
