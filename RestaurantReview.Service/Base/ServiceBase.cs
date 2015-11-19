using RestaurantReview.BL.Base;
using RestaurantReview.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantReview.DAL.Base;
using RestaurantReview.DAL.Interface;
using System.Data.Entity;
using RestaurantReview.Common.Logger;
using RestaurantReview.DAL.Context;

namespace RestaurantReview.Service.Base
{
    public abstract class ServiceBase<M, D> : LoggerBase, IService<M, D> where M : ModelBase where D : EntityBase, IEntityBase<D>
    {
        protected IRRContext _context;
        protected IDbSet<D> _dbset;

        public ServiceBase(IRRContext context)
        {
            _context = context;
            _dbset = _context.Set<D>();
        }

        public ServiceBase()
        {
            _context = new RR();
            _dbset = _context.Set<D>();
        }

        public virtual int Add(M model, int userID)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            D entity = Convert(model);

            entity = _dbset.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }


        public virtual void Update(M model, int userID)
        {
            if (model == null) throw new ArgumentNullException("model");
            _context.Entry(Convert(model)).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        internal virtual void Delete(D model)
        {
            if (model == null) throw new ArgumentNullException("model");
            _dbset.Remove(model);
            _context.SaveChanges();
        }

        public virtual void Delete(M model, int userID)
        {
             throw new NotImplementedException();
        }

        public virtual IEnumerable<M> GetAll()
        {
            return Convert(_dbset.AsEnumerable<D>());
        }

        internal virtual D Convert(M model){
            D returnObject = null;

            AutoMapper.Mapper.CreateMap<M, D>();
            returnObject = AutoMapper.Mapper.Map<M, D>(model, returnObject);

            return returnObject;
        }
        internal virtual M Convert(D entity)
        {
            M returnObject = null;

            AutoMapper.Mapper.CreateMap<D, M>();
            returnObject = AutoMapper.Mapper.Map<D, M>(entity, returnObject);

            return returnObject;
        }
        internal virtual IEnumerable<M> Convert(IEnumerable<D> entity)
        {
            IEnumerable<M> returnObject = null;

            AutoMapper.Mapper.CreateMap<D, M>();
            returnObject = AutoMapper.Mapper.Map<IEnumerable<D>, IEnumerable<M>>(entity, returnObject);

            return returnObject;
        }
    }
}