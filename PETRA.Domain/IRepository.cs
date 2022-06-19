using System.Linq.Expressions;
using PETRA.Domain.Entities;

namespace PETRA.Domain
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> filter);
        // Task<T> Get(int id);
        Task<T>? Add(T model);
        // Task<T>? Upsert(T model);
        // Task<bool> Delete(int id);
        // Task<bool> Delete(IEnumerable<int> ids);
    }
}
