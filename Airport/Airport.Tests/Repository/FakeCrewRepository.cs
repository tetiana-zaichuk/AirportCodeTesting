using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace Airport.Tests.Repository
{
    public class FakeCrewRepository : IRepository<Crew> 
    {
        public readonly List<Crew> Data;

        public FakeCrewRepository(params Crew[] data)
        {
            Data = new List<Crew>(data);
        }

        public virtual List<Crew> Get(int? filter = null)
        {
            List<Crew> query=Data;
            if (filter != null)
            {
                query = query.Where(e => e.Id == filter).ToList();
            }

            return query;
        }

        public virtual void Create(Crew entity, string createdBy = null)
        {
           Data.Add(entity);
        }

        public virtual void Update(Crew entity, string modifiedBy = null)
        {
            
        }

        public virtual void Delete(int? filter = null)
        {
            List<Crew> query = Data;

            if (filter != null)
            {
                Delete(query.Find(e => e.Id == filter));
            }
            else
            {
                query.Clear();
            }
        }

        public virtual void Delete(Crew entity)
        {
            Data.Remove(entity);
        }
    }
}
