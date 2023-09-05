using StudyManagement.Domain.Commons;
using System.Linq.Expressions;

namespace StudyManagement.Data.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task CreateAsync(T entity);
    void Update(T entiy);
    void Delete(T entity);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, bool isNoTracked = true, string[] includes = null);
    Task SaveAsync();
}
