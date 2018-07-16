using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace Airport.Tests.Repository
{
    public class FakeFlightRepository : IRepository<Flight>
    {
        public readonly List<Flight> Data;

        public FakeFlightRepository(params Flight[] data)
        {
            Data = new List<Flight>(data);
        }

        public virtual List<Flight> Get(int? filter = null)
        {
            List<Flight> query=Data;
            if (filter != null)
            {
                query = query.Where(e => e.Id == filter).ToList();
            }

            return query;
        }

        public virtual void Create(Flight entity, string createdBy = null)
        {
           Data.Add(entity);
        }

        public virtual void Update(Flight entity, string modifiedBy = null)
        {
            
        }

        public virtual void Delete(int? filter = null)
        {
            List<Flight> query = Data;

            if (filter != null)
            {
                Delete(query.Find(e => e.Id == filter));
            }
            else
            {
                query.Clear();
            }
        }

        public virtual void Delete(Flight entity)
        {
            Data.Remove(entity);
        }
    }
}
