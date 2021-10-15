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
    /// <typeparam name="TDTO">Type of Dto object</typeparam>
    public class MapperRepository<TEntity, TDTO> : Repository<TEntity>, IMapperRepository<TEntity, TDTO> where TEntity : Entity
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
        public virtual IEnumerable<TDTO> GetAllMapped()
        {
            return Mapper.Map<List<TDTO>>(GetAll());
        }

        /// <inheritdoc />
        public virtual IEnumerable<TDTO> GetMappedList(Expression<Func<TEntity, bool>> expression)
        {
            return Mapper.Map<List<TDTO>>(GetList(expression));
        }

        /// <inheritdoc />
        public virtual TDTO GetMapped(int id)
        {
            return Mapper.Map<TDTO>(Get(id));
        }

        /// <inheritdoc />
        public virtual TEntity CreateByModel<TModel>(TModel entity)
        {
            return Create(Mapper.Map<TEntity>(entity));
        }

        /// <inheritdoc />
        public virtual int CreateAndSaveByModel<TModel>(TModel entity)
        {
            return CreateAndSave(Mapper.Map<TEntity>(entity));
        }

        /// <inheritdoc />
        public virtual void UpdateByModel<TModel>(int id, TModel model)
        {
            Update(Mapper.Map(model, Get(id)));
        }

        /// <inheritdoc />
        public virtual void UpdateAndSaveByModel<TModel>(int id, TModel model)
        {
            UpdateAndSave(Mapper.Map(model, Get(id)));
        }
    }
}