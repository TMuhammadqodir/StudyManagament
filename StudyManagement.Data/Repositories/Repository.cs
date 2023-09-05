using Microsoft.EntityFrameworkCore;
using StudyManagement.Data.DbContexts;
using StudyManagement.Data.IRepositories;
using StudyManagement.Domain.Commons;
using System.Linq.Expressions;

namespace StudyManagement.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext _appDbContext;

    private readonly DbSet<T> dbSet;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        dbSet = appDbContext.Set<T>();
    }
    
    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public void Update(T entiy)
    {
        entiy.UpdateAt = DateTime.UtcNow;
        dbSet.Entry(entiy).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        IQueryable<T> query = expression is null ? dbSet.AsQueryable() : dbSet.Where(expression).AsQueryable();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        var result = await query.FirstOrDefaultAsync(expression);

        return result;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, bool isNoTracked = true, string[] includes = null)
    {
        IQueryable<T> query = expression is null ? dbSet.AsQueryable() : dbSet.Where(expression).AsQueryable();

        query = isNoTracked ? query.AsNoTracking() : query;

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return query;
    }

    public async Task SaveAsync()
        => await _appDbContext.SaveChangesAsync();
}
