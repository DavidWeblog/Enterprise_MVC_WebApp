using Enterprise_MVC_WebApplication.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Enterprise_MVC_WebApplication.Infra.Repositories
{
    public class GenericTypeRepository<T> : IGenericTypeRepository<T> where T : class
    {
        private TestEntities dbContext = new TestEntities();

        /// <summary>
        /// Get All information.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAll()
        {
            return Task.Run(() => dbContext.Set<T>().ToList() as IEnumerable<T>);
        }

        /// <summary>
        /// Async Get All information.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> AsyncGetAll()
        {
            return await Task.Run(() => dbContext.Set<T>().ToList() as IEnumerable<T>);
        }

        /// <summary>
        /// Find data by where conitions.
        /// </summary>
        /// <param name="predicate">Where conditions</param>
        /// <returns>T</returns>
        public Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() => dbContext.Set<T>().Where(predicate).FirstOrDefault());
        }

        /// <summary>
        /// Async Find data by where conitions.
        /// </summary>
        /// <param name="predicate">Where conditions</param>
        /// <returns>T</returns>
        public async Task<T> AsyncFind(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => dbContext.Set<T>().Where(predicate).FirstOrDefault());
        }

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id">int32</param>
        /// <returns> T </returns>
        public Task<T> GetById(int id)
        {
            if (id != 0)
                return Task.Run(() => dbContext.Set<T>().Find(id) as T);
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Async Get By ID.
        /// </summary>
        /// <param name="id">int32</param>
        /// <returns> T </returns>
        public async Task<T> AsyncGetById(int id)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            object[] keyValueObj = new object[] { id };

            if (id != 0)
                return await Task.Run(() => dbContext.Set<T>().FindAsync(cancellationToken: token, keyValueObj) as T);
            else
                return null;
        }

        /// <summary>
        /// Create new data.
        /// </summary>
        /// <param name="entity">entity model class</param>
        /// <returns></returns>
        public Task Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return Task.Run(() => dbContext.SaveChanges());
        }

        /// <summary>
        /// Async Create new data.
        /// </summary>
        /// <param name="entity">entity model class</param>
        /// <returns></returns>
        public async Task AsyncCreate(T entity)
        {
            try
            {
                dbContext.Set<T>().Add(entity);
            }
            catch (Exception e)
            { }
            finally {
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update data.
        /// </summary>
        /// <param name="entity">entity model class</param>
        /// <returns></returns>
        public Task Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            SaveChange();
            return Task.FromResult<bool>(true);
        }

        /// <summary>
        /// Async Update data
        /// </summary>
        /// <param name="entity">entity model class</param>
        /// <returns></returns>
        public async Task AsyncUpdate(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangeAsync();
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="entity">entity model class</param>
        /// <returns></returns>
        public Task Delete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            SaveChange();
            return Task.FromResult<bool>(true);
        }

        /// <summary>
        /// Async Delete data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AsyncDelete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            await SaveChangeAsync();
        }

        /// <summary>
        /// Row count
        /// </summary>
        /// <param name="predicate">Where conditions</param>
        /// <returns>int32</returns>
        public Task<int> RowCount(Func<T, bool> predicate)
        {
            return Task.Run(() => dbContext.Set<T>().Where(predicate).Count());
        }

        /// <summary>
        /// Async Row count
        /// </summary>
        /// <param name="predicate">Where conditions</param>
        /// <returns>int32</returns>
        public async Task<int> AsyncRowCount(Func<T, bool> predicate)
        {
            return await Task.Run(() => dbContext.Set<T>().Where(predicate).Count());
        }

        /// <summary>
        /// Save Change
        /// </summary>
        /// <returns></returns>
        public Task SaveChange()
        {
            dbContext.SaveChanges();
            return null;
        }

        /// <summary>
        /// Async Save Change
        /// </summary>
        /// <returns></returns>
        public Task SaveChangeAsync()
        {
            dbContext.SaveChangesAsync();
            return null;
        }
    }
}
