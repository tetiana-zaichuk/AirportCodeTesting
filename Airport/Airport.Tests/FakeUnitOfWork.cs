using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Airport.Tests.Repository;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace Airport.Tests
{
    public class FakeUnitOfWork// : IUnitOfWork
    {
        public readonly List<Entity> Data;
        private FakeCrewRepository _crewRepository;
        private FakeFlightRepository _flightRepository;
        private FakeAircraftRepository _aircraftRepository;
        private FakeDepartureRepository _departureRepository;

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        /*public FakeUnitOfWork(IRepository<Entity> _repository)
        {
            SetRepository<Entity>(_repository);
        }*/

        public void SetRepository<TEntity>(IRepository<TEntity> repository) where TEntity : Entity
        {
            _repositories[typeof(TEntity)] = repository;
        }

        
        //public CrewRepository CrewRepository => _crewRepository ?? (_crewRepository = new CrewRepository(Data));

        //public FlightRepository FlightRepository => _flightRepository ?? (_flightRepository = new FlightRepository(Data));

        //public AircraftRepository AircraftRepository => _aircraftRepository ?? (_aircraftRepository = new AircraftRepository(Data));

        //public DepartureRepository DepartureRepository => _departureRepository ?? (_departureRepository = new DepartureRepository(Data));

        public IRepository<TEntity> Set<TEntity>() where TEntity : Entity
        {
            object repository;
            return _repositories.TryGetValue(typeof(TEntity), out repository)
                ? (IRepository<TEntity>)repository
                : new FakeRepository<TEntity>();
        }

        public int SaveChages()
        {
            return 0;
        }

        //public Task<int> SaveChangesAsync()
        //{
        //    return 0;
        //}

    }
}
