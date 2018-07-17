﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class DepartureRepository : IRepository<Departure>
    {
        protected readonly AirportContext Context;

        public DepartureRepository(AirportContext context)
        {
            Context = context;
        }

        public virtual List<Departure> Get(int? filter = null)
        {
            IQueryable<Departure> query = Context.Set<Departure>().Include(c => c.Crew).Include(a=>a.Aircraft).Include(f=>f.Flight);

            if (filter != null)
            {
                query = query.Where(e => e.Id == filter);
            }

            return query.ToList();
        }

        public virtual void Create(Departure entity, string createdBy = null)
        {
            Context.Set<Departure>().Add(entity);
        }

        public virtual void Update(Departure entity, string modifiedBy = null)
        {
            Departure oldEntity = Context.Set<Departure>().Find(entity.Id);
            Context.Entry(oldEntity).State = EntityState.Detached;
            //Context.Set<TEntity>().Update(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int? filter = null)
        {
            List<Departure> query = Context.Set<Departure>().ToList();

            if (filter != null)
            {
                Delete(query.Find(e => e.Id == filter));
            }
            else
            {
                Context.Set<Departure>().RemoveRange(query);
            }
        }

        public virtual void Delete(Departure entity)
        {
            var dbSet = Context.Set<Departure>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
    }
}
