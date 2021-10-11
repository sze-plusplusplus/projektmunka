using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MeetHut.DataAccess.Entities;

namespace MeetHut.DataAccess.Repositories
{
    /// <inheritdoc />
    public class Repository<T> : IRepository<T> where T : Entity
    {
        /// <summary>
        /// Database Context
        /// </summary>
        protected readonly DatabaseContext DatabaseContext;
        
        /// <summary>
        /// Init default repository
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        public Repository(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        
        /// <inheritdoc />
        public IEnumerable<T> GetAll()
        {
            return DatabaseContext.Set<T>().ToList();
        }

        /// <inheritdoc />
        public IEnumerable<T> GetList(Expression<Func<T, bool>> expression)
        {
            return DatabaseContext.Set<T>().Where(expression).ToList();
        }

        /// <inheritdoc />
        public T Get(int id)
        {
            var el = DatabaseContext.Set<T>().Find(id);

            if (el == null)
            {
                throw new ArgumentException($"Element not found with id: {id}");
            }
            
            return el;
        }

        /// <inheritdoc />
        public T Create(T entity)
        {
            return DatabaseContext.Set<T>().Add(entity).Entity;
        }

        /// <inheritdoc />
        public int CreateAndSave(T entity)
        {
            var newEntity = Create(entity);
            Complete();
            return newEntity.Id;
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            DatabaseContext.Set<T>().Update(entity);
        }

        /// <inheritdoc />
        public void UpdateAndSave(T entity)
        {
            Update(entity);
            Complete();
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public void DeleteById(int id)
        {
            Delete(Get(id));
        }

        /// <inheritdoc />
        public void DeleteAndSave(T entity)
        {
            Delete(entity);
            Complete();
        }

        /// <inheritdoc />
        public void DeleteByIdAndSave(int id)
        {
            DeleteById(id);
            Complete();
        }

        /// <inheritdoc />
        public void Complete()
        {
            DatabaseContext.SaveChanges();
        }
    }
}