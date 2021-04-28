using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using XMarketPlace.Core.Entity;

namespace XMarketPlace.Core.Service
{
    public interface ICoreService<T> where T:CoreEntity
    {
        bool Add(T item);
        bool Add(List<T> items);
        bool Update(T item);
        bool Remove(T item);
        bool Remove(Guid id);
        bool RemoveAll(Expression<Func<T, bool>> expression); // Şarta uyan kayıtları bul ve sil
        T GetById(Guid id);
        T GetByDefault(Expression<Func<T, bool>> expression);
        List<T> GetActive();
        List<T> GetDefault(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        bool Activate(Guid id);
        bool Any(Expression<Func<T, bool>> expression);
        int Save();
    }
}
