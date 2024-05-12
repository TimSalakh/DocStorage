﻿using API.DAL.Contexts;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.DAL.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly DocStorageDbContext _context;

    public UserRepository(DocStorageDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IQueryable<User>> GetAllAsync()
    {
        return await Task.Run(() =>
        {
            return _context.User
            .AsQueryable()
            .AsNoTracking();
        });
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.User.FindAsync(id);
    }
}
