using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        CrewRepository CrewRepository { get; }
        FlightRepository FlightRepository { get; }
        AircraftRepository AircraftRepository { get; set; }
        DepartureRepository DepartureRepository { get; }

        IRepository<TEntity> Set<TEntity>() where TEntity : Entity;

        int SaveChages();

        Task<int> SaveChangesAsync();
    }
}
