using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace Airport.Tests.Repository
{
    public class FakeAircraftRepository : IRepository<Aircraft>
    {
        public readonly List<Aircraft> Data;

        public FakeAircraftRepository(params Aircraft[] data)
        {
            Data = new List<Aircraft>(data);
        }

        public virtual List<Aircraft> Get(int? filter = null)
        {
            List<Aircraft> query=Data;
            if (filter != null)
            {
                query = query.Where(e => e.Id == filter).ToList();
            }

            return query;
        }

        public virtual void Create(Aircraft entity, string createdBy = null)
        {
           Data.Add(entity);
        }

        public virtual void Update(Aircraft entity, string modifiedBy = null)
        {
            
        }

        public virtual void Delete(int? filter = null)
        {
            List<Aircraft> query = Data;

            if (filter != null)
            {
                Delete(query.Find(e => e.Id == filter));
            }
            else
            {
                query.Clear();
            }
        }

        public virtual void Delete(Aircraft entity)
        {
            Data.Remove(entity);
        }
    }
}
