using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace Airport.Tests.Repository
{
    public class FakeDepartureRepository : IRepository<Departure>
    {
        public readonly List<Departure> Data;

        public FakeDepartureRepository(params Departure[] data)
        {
            Data = new List<Departure>(data);
        }

        public virtual List<Departure> Get(int? filter = null)
        {
            List<Departure> query=Data;
            if (filter != null)
            {
                query = query.Where(e => e.Id == filter).ToList();
            }

            return query;
        }

        public virtual void Create(Departure entity, string createdBy = null)
        {
           Data.Add(entity);
        }

        public virtual void Update(Departure entity, string modifiedBy = null)
        {
            
        }

        public virtual void Delete(int? filter = null)
        {
            List<Departure> query = Data;

            if (filter != null)
            {
                Delete(query.Find(e => e.Id == filter));
            }
            else
            {
                query.Clear();
            }
        }

        public virtual void Delete(Departure entity)
        {
            Data.Remove(entity);
        }
    }
}
