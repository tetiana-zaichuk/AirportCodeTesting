using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Shared.DTO;

namespace BusinessLayer.Services
{
    public class DepartureService : IService<Departure>
    {
        private readonly UnitOfWork _unitOfWork;

        public DepartureService(AirportContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public bool ValidationForeignId(Departure ob)
        {
            return _unitOfWork.Set<DataAccessLayer.Models.Aircraft>().Get()
                        .FirstOrDefault(o => o.Id == ob.Aircraft.Id) != null &&
                    _unitOfWork.Set<DataAccessLayer.Models.Crew>().Get().FirstOrDefault(o => o.Id == ob.Crew.Id) !=
                    null &&
                    _unitOfWork.Set<DataAccessLayer.Models.Flight>().Get().FirstOrDefault(o => o.Id == ob.Flight.Id) !=
                    null;
        }

        public Departure IsExist(int id)
            => Mapper.Map<DataAccessLayer.Models.Departure, Departure>(_unitOfWork.DepartureRepository.Get(id).FirstOrDefault());

        public DataAccessLayer.Models.Departure ConvertToModel(Departure departure)
            => Mapper.Map<Departure, DataAccessLayer.Models.Departure>(departure);

        public List<Departure> GetAll()
            => Mapper.Map<List<DataAccessLayer.Models.Departure>, List<Departure>>(_unitOfWork.DepartureRepository.Get());

        public Departure GetDetails(int id) => IsExist(id);

        public void Add(Departure departure)
        {
            _unitOfWork.DepartureRepository.Create(ConvertToModel(departure));
            _unitOfWork.SaveChages();
        }

        public void Update(Departure departure)
        {
            _unitOfWork.DepartureRepository.Update(ConvertToModel(departure));
            _unitOfWork.SaveChages();
        }

        public void Remove(int id)
        {
            _unitOfWork.DepartureRepository.Delete(id);
            _unitOfWork.SaveChages();
        }

        public void RemoveAll()
        {
            _unitOfWork.Set<DataAccessLayer.Models.Departure>().Delete();
            _unitOfWork.SaveChages();
        }
    }
}
