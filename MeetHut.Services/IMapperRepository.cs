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
    public interface IMapperRepository<TEntity, out TDTO> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Get all mapped element
        /// </summary>
        /// <returns>List of element of the given type</returns>
        IEnumerable<TDTO> GetAllMapped();

        /// <summary>
        /// Get mapped list of elements filtered by the given expression
        /// </summary>
        /// <param name="expression">Expression</param>
        /// <returns>Filtered list of the given type</returns>
        IEnumerable<TDTO> GetMappedList(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Get mapped element by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Element or default value</returns>
        TDTO GetMapped(int id);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="model">Model object</param>
        /// <returns>The newly created object's Id</returns>
        TEntity CreateByModel<TModel>(TModel model);

        /// <summary>
        /// Create entity and perform save
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="model">Model object</param>
        /// <returns>The newly created object's Id</returns>
        int CreateAndSaveByModel<TModel>(TModel model);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="id">Id</param>
        /// <param name="model">Model object</param>
        void UpdateByModel<TModel>(int id, TModel model);

        /// <summary>
        /// Update entity and perform save
        /// </summary>
        /// <typeparam name="TModel">Type of model</typeparam>
        /// <param name="id">Id</param>
        /// <param name="model">Model object</param>
        void UpdateAndSaveByModel<TModel>(int id, TModel model);
    }
}