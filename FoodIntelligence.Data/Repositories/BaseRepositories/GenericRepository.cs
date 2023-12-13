using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FoodIntelligence.Data.Repositories.BaseRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FIntelligenceDbContext _dbContext;

        public GenericRepository(FIntelligenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }
        public IEnumerable<T> GetAllInclude(string includeProperties)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                => current.Include(includeProperty));

            return query.ToList();
        }
        public IQueryable<T> FindQueryable(Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _dbContext.Set<T>().Where(expression);
            return orderBy != null ? orderBy(query) : query;
        }

        public Task<List<T>> FindListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>,
            IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            var query = expression != null ? _dbContext.Set<T>().Where(expression) : _dbContext.Set<T>();
            return orderBy != null
                ? orderBy(query).ToListAsync(cancellationToken)
            : query.ToListAsync(cancellationToken);
        }

        public Task<List<T>> FindAllAsync(CancellationToken cancellationToken)
        {
            return _dbContext.Set<T>().ToListAsync(cancellationToken);
        }


        public Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, string includeProperties)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            query = includeProperties.Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
                => current.Include(includeProperty));

            return query.SingleOrDefaultAsync(expression);
        }

        public T Add(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }    

    }
}
