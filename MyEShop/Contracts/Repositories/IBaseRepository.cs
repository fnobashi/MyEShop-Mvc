using MyEShop.Models;
using MyEShop.Models.Entities;

namespace MyEShop.Contracts.Repositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Methods

        IQueryable<TEntity> GetAll();

        TEntity GetById(TKey id);

        void Add(TEntity entity);

        void Delete(TKey id);

        void Update(TEntity entity);

        #endregion Methods

        #region AsyncMethods

        Task<TEntity> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        Task DeleteAsync(TKey id);

        Task UpdateAsync(TEntity entity);

        #endregion AsyncMethods

        IQueryable<TEntity> GetAll(SortBy sort);
        void Save();
        Task SaveAsync();

    }
}