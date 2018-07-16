using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace Airport.Tests.Repository
{
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public readonly List<TEntity> Data;

        public FakeRepository(params TEntity[] data)
        {
            Data = new List<TEntity>(data);
        }

        public virtual List<TEntity> Get(int? filter = null)
        {
            List<TEntity> query=Data;
            if (filter != null)
            {
                query = query.Where(e => e.Id == filter).ToList();
            }

            return query;
        }

        public virtual void Create(TEntity entity, string createdBy = null)
        {
           Data.Add(entity);
        }

        public virtual void Update(TEntity entity, string modifiedBy = null)
        {
            
        }

        public virtual void Delete(int? filter = null)
        {
            List<TEntity> query = Data;

            if (filter != null)
            {
                Delete(query.Find(e => e.Id == filter));
            }
            else
            {
                query.Clear();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            Data.Remove(entity);
        }
    }
}
