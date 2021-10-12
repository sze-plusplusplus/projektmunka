using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Repositories;

namespace MeetHut.Services
{
    /// <summary>
    /// Mapper Repository
    /// </summary>
    public interface IMapperRepository<TEntity, out TDto> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Get all mapped element
        /// </summary>
        /// <returns>List of element of the given type</returns>
        IEnumerable<TDto> GetAllMapped();

        /// <summary>
        /// Get mapped list of elements filtered by the given expression
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>Filtered list of the given type</returns>
        IEnumerable<TDto> GetMappedList(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Get mapped element by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Element or default value</returns>
        TDto GetMapped(int id);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="model">Model object</param>
        /// <returns>The newly created object's Id</returns>
        TEntity Create<TModel>(TModel model);

        /// <summary>
        /// Create entity and perform save
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="model">Model object</param>
        /// <returns>The newly created object's Id</returns>
        int CreateAndSave<TModel>(TModel model);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="id">Id</param>
        /// <param name="model">Model object</param>
        void Update<TModel>(int id, TModel model);

        /// <summary>
        /// Update entity and perform save
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="id">Id</param>
        /// <param name="model">Model object</param>
        void UpdateAndSave<TModel>(int id, TModel model);
    }
}