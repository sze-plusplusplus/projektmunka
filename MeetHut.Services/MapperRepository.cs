using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using MeetHut.DataAccess;
using MeetHut.DataAccess.Entities;
using MeetHut.DataAccess.Repositories;

namespace MeetHut.Services
{
    /// <summary>
    /// Mapper Repository
    /// </summary>
    /// <typeparam name="TEntity">Type of database entity</typeparam>
    /// <typeparam name="TDto">Type of Dto object</typeparam>
    /// <typeparam name="TModel">Type of Model object</typeparam>
    public class MapperRepository<TEntity, TDto, TModel> : Repository<TEntity>, IMapperRepository<TEntity, TDto, TModel> where TEntity : Entity
    {
        /// <summary>
        /// Mapper
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// Init Mapper Repository
        /// </summary>
        /// <param name="databaseContext">Database context</param>
        /// <param name="mapper">Mapper</param>
        public MapperRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            Mapper = mapper;
        }

        /// <inheritdoc />
        public virtual IEnumerable<TDto> GetAllMapped()
        {
            return Mapper.Map<List<TDto>>(GetAll());
        }

        /// <inheritdoc />
        public virtual IEnumerable<TDto> GetMappedList(Expression<Func<TEntity, bool>> expression)
        {
            return Mapper.Map<List<TDto>>(GetList(expression));
        }

        /// <inheritdoc />
        public virtual TDto GetMapped(int id)
        {
            return Mapper.Map<TDto>(Get(id));
        }

        /// <inheritdoc />
        public virtual TEntity Create(TModel entity)
        {
            return Create(Mapper.Map<TEntity>(entity));
        }

        /// <inheritdoc />
        public virtual int CreateAndSave(TModel entity)
        {
            return CreateAndSave(Mapper.Map<TEntity>(entity));
        }

        /// <inheritdoc />
        public virtual void Update(int id, TModel model)
        {
            Update(Mapper.Map(model, Get(id)));
        }

        /// <inheritdoc />
        public virtual void UpdateAndSave(int id, TModel model)
        {
            UpdateAndSave(Mapper.Map(model, Get(id)));
        }
    }
}