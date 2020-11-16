using Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Common
{
    public class BaseDAL<T> where T : class
    {
        private static TourDbContext context = new TourDbContext();
        public static IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
                IQueryable<T> query = context.Set<T>();

                if (includeProperties != null)
                {
                    includeProperties.ForEach(i => query = query.Include(i));
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);

                }

                if (page != null && pageSize != null)
                {
                    query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                return query.ToList();
        }
        public static T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public static IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).AsNoTracking().ToList();
        }
        public static void Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }
        public static void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }
        public static void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
