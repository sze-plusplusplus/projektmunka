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
        private readonly DatabaseContext _databaseContext;
        
        /// <summary>
        /// Init default repository
        /// </summary>
        /// <param name="databaseContext">Database Context</param>
        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        /// <inheritdoc />
        public IEnumerable<T> GetAll()
        {
            return _databaseContext.Set<T>().ToList();
        }

        /// <inheritdoc />
        public IEnumerable<T> GetList(Expression<Func<T, bool>> expression)
        {
            return _databaseContext.Set<T>().Where(expression).ToList();
        }

        /// <inheritdoc />
        public T Get(int id)
        {
            return _databaseContext.Set<T>().Find(id);
        }

        /// <inheritdoc />
        public int Create(T entity)
        {
            return _databaseContext.Set<T>().Add(entity).Entity.Id;
        }

        /// <inheritdoc />
        public int CreateAndSave(T entity)
        {
            var id = Create(entity);
            Complete();
            return id;
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            _databaseContext.Set<T>().Update(entity);
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
            _databaseContext.Set<T>().Remove(entity);
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
            _databaseContext.SaveChanges();
        }
    }
}