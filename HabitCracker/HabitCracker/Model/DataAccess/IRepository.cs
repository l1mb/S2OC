using System.Collections.Generic;

namespace HabitCracker.Model.DataAccess
{
    public interface IRepository
    {
        void SaveChanges();
    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        void Remove(TEntity entity);

        void RemoveAll();

        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
    }
}