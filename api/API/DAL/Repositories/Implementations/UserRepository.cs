using API.DAL.Contexts;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DocStorageDbContext context) 
        : base(context) { }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _context.User
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task ConfirmEmailAsync(User user)
    {
        if (user.IsEmailConfirmed) 
            return;

        user.IsEmailConfirmed = true;
        _context.User.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task SetRoleAsync(User user, Role role)
    {
        user.Role = role; 
        _context.User.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string email)
    {
        var user = await FindByEmailAsync(email);
        if (user == null)
            return;
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
    }
}
