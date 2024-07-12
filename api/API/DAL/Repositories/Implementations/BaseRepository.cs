using API.DAL.Contexts;
using API.DAL.Repositories.Interfaces;

namespace API.DAL.Repositories.Implementations;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
    where TEntity : class
{
    protected readonly DocStorageDbContext _context;

    public BaseRepository(DocStorageDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await FindByIdAsync(id);
        if (entity == null)
            return;
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return await Task.Run(() =>
        {
            return _context.Set<TEntity>()
            .AsQueryable();
        });
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
