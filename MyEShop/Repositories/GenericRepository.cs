using Microsoft.EntityFrameworkCore;
using MyEShop.Contracts.Repositories;
using MyEShop.Models;
using MyEShop.Models.Entities;

namespace MyEShop.Repositories
{
    public abstract class GenericRepository<TEntity, Tkey> : IBaseRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly DbSet<TEntity> dbset;
        private readonly DbContext context;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            context.Add(entity);
            context.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual void Delete(Tkey id)
        {
            var entity = dbset.Find(id);
            entity.DeletedDate = DateTime.Now;
            entity.IsDeleted = true;
            context.SaveChanges();
        }

        public virtual async Task DeleteAsync(Tkey id)
        {
            var entity = await dbset.FindAsync(id);
            entity.DeletedDate = DateTime.Now;
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
            => dbset;


        public virtual TEntity GetById(Tkey id)
            => dbset.Find(id);

        public virtual async Task<TEntity> GetByIdAsync(Tkey id)
            => await dbset.FindAsync(id);

        public virtual void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            context.Update(entity);
            context.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        public abstract IQueryable<TEntity> GetAll(SortBy sort);



    }
}
