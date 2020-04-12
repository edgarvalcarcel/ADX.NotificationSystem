/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ADX.DataAccessLayer
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual void Add(params T[] items)
        {
            using (var context = new Entities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }

                context.SaveChanges();
            }
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new Entities())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                list = dbQuery.AsNoTracking().ToList();
            }

            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new Entities())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                list = dbQuery.AsNoTracking().AsEnumerable().Where(where).ToList();
            }

            return list;
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item;
            using (var context = new Entities())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                //Apply eager loading
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }

            return item;
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new Entities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }

                context.SaveChanges();
            }
        }

        public virtual void Update(params T[] items)
        {
            using (var context = new Entities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }
    }
}