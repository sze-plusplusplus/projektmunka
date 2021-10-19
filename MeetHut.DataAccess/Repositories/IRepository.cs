using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MeetHut.DataAccess.Entities;

namespace MeetHut.DataAccess.Repositories
{
    /// <summary>
    /// Default repository management
    /// </summary>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Get all element
        /// </summary>
        /// <returns>List of element of the given type</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get list of elements filtered by the given expression
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>Filtered list of the given type</returns>
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get element by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Element or default value</returns>
        T Get(int id);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>The newly created object's Id</returns>
        T Create(T entity);

        /// <summary>
        /// Create entity and perform save
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>The newly created object's Id</returns>
        int CreateAndSave(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(T entity);

        /// <summary>
        /// Update entity and perform save
        /// </summary>
        /// <param name="entity">Entity object</param>
        void UpdateAndSave(T entity);

        /// <summary>
        /// Delete by entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(T entity);

        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <param name="id">Id</param>
        void DeleteById(int id);

        /// <summary>
        /// Delete by entity and perform save
        /// </summary>
        /// <param name="entity">Entity object</param>
        void DeleteAndSave(T entity);

        /// <summary>
        /// Delete by Id and perform save
        /// </summary>
        /// <param name="id">Id</param>
        void DeleteByIdAndSave(int id);

        /// <summary>
        /// Persist database actions
        /// </summary>
        void Complete();
    }
}