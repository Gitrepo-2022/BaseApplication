using BaseApplication.Repository.Data;
using BaseApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseApplication.Repository.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal BaseApplicationDbContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(BaseApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool ignoreLanguage = false)
        {
            var query = GetQuery(filter, includeProperties, ignoreLanguage);

            if (orderBy != null)
                return orderBy(query).ToList();


            return query.ToList();
        }

        public virtual List<TEntity> Get(int limit, int skip, Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool ignoreLanguage = false)
        {
            var query = GetQuery(filter, includeProperties, ignoreLanguage);

            if (orderBy != null)
                return orderBy(query).Skip(skip).Take(limit).ToList();
            return query.Skip(skip).Take(limit).ToList();
        }

        public virtual List<TResult> GetAs<TResult>(Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "", bool ignoreLanguage = false) where TResult : class
        {
            var query = GetQuery(filter, includeProperties);
            return query.Select(select).ToList();
        }
        public virtual Task<List<TResult>> GetAsAsync<TResult>(Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "") where TResult : class
        {
            var query = GetQuery(filter, includeProperties);
            return query.Select(select).ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool ignoreLanguage = false)
        {

            var query = GetQuery(filter, includeProperties);

            if (orderBy != null)
                return orderBy(query).ToListAsync();

            return query.ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(int limit, int skip, Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = ""
          )
        {

            var query = GetQuery(filter, includeProperties);

            if (orderBy != null)
                return orderBy(query).Skip(skip).Take(limit).ToListAsync();

            return query.Skip(skip).Take(limit).ToListAsync();
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = GetQuery(filter);
            var count = query.Count();
            return count;
        }

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "", bool ignoreLanguage = false)
        {
            IQueryable<TEntity> query = dbSet;

            Type[] types = typeof(TEntity).GetInterfaces();

            //here we re filter deleted item


            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }


        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.FirstOrDefault(filter);
        }
        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.FirstOrDefaultAsync(filter);
        }

        public virtual void Insert(TEntity entity, bool SaveChanges = true)
        {
            dbSet.Add(entity);
            if (SaveChanges == true)
                context.SaveChanges();
        }

        public virtual void InsertAll(List<TEntity> entities, bool SaveChanges = true)
        {
            foreach (var entity in entities)
            {
                dbSet.Add(entity);
            }
            if (SaveChanges == true)
                context.SaveChanges();
        }

        public virtual void DeleteAll(List<TEntity> entities, bool SaveChanges = true)
        {
            foreach (var entityToDelete in entities)
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }

                else
                {
                    dbSet.Remove(entityToDelete);
                }
            }
            if (SaveChanges == true)
                context.SaveChanges();
        }

        public virtual void UpdateAll(List<TEntity> entities, bool SaveChanges = true)
        {
            foreach (var entityToUpdate in entities)
            {
                if (context.Entry(entityToUpdate).State == EntityState.Detached)
                    dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            if (SaveChanges == true)
                context.SaveChanges();
        }

        public virtual void Delete(object id, bool SaveChanges = true)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete, SaveChanges);
        }

        public virtual void Delete(TEntity entityToDelete, bool SaveChanges = true)
        {

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            else
            {
                dbSet.Remove(entityToDelete);
            }
            if (SaveChanges == true)
                context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate, bool SaveChanges = true)
        {
            if (context.Entry(entityToUpdate).State == EntityState.Detached)
                dbSet.Attach(entityToUpdate);

            context.Entry(entityToUpdate).State = EntityState.Modified;
            if (SaveChanges == true)
                context.SaveChanges();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Any(filter);
        }
        public bool Exists(Expression<Func<TEntity, bool>> whereCondition)
        {
            return dbSet.Any(whereCondition);
        }
        public IEnumerable<TEntity> GetAll()
        {

            return dbSet.AsEnumerable();

        }
        public IQueryable<TEntity> GetAsQueryable()
        {

            return dbSet.AsQueryable<TEntity>();

        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).AsEnumerable();
        }
        public IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).AsQueryable<TEntity>();
        }

    }
}