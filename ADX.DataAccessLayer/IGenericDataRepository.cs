/**************************************************************************
ADX 365 - Tech team
**************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ADX.DataAccessLayer
{
    public interface IGenericDataRepository<T> where T : class
    {
        void Add(params T[] items);

        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        void Remove(params T[] items);

        void Update(params T[] items);
    }
}