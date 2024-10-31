using BHD.Domain.Models;
using System.Linq.Expressions;

namespace BHD.Application.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        T Update(T entity);
        T Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Exists(Expression<Func<T, bool>> predicate);

    }
}
