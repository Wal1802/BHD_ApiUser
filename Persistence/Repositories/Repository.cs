using BHD.Application.Repositories;
using BHD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BHD.Persistence.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        

        public T Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.ModifiedAt = DateTime.Now;
            _dbSet.Add(entity);

            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
            }
            _dbSet.AddRange(entities);

            _context.SaveChanges();
            return entities;
        }

        public T Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Exists(Expression<Func<T, bool>> predicate) => _dbSet.Any(predicate);

        public IEnumerable<T> GetAll() => _dbSet;

        public T GetById(int id) => _dbSet.Find(id);

        public T Update(T entity)
        {
            entity.ModifiedAt = DateTime.Now;
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
