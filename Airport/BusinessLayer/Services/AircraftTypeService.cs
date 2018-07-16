using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Shared.DTO;

namespace BusinessLayer.Services
{
    public class AircraftTypeService : IService<AircraftType>
    {
        private readonly UnitOfWork _unitOfWork;
        
        public AircraftTypeService(AirportContext context)
            => _unitOfWork = new UnitOfWork(context);

        public bool ValidationForeignId(AircraftType ob) => true;

        public AircraftType IsExist(int id)
            => Mapper.Map<DataAccessLayer.Models.AircraftType, AircraftType>(_unitOfWork.Set<DataAccessLayer.Models.AircraftType>().Get(id).FirstOrDefault());

        public DataAccessLayer.Models.AircraftType ConvertToModel(AircraftType aircraftType)
            => Mapper.Map<AircraftType, DataAccessLayer.Models.AircraftType>(aircraftType);

        public List<AircraftType> GetAll()
            => Mapper.Map<List<DataAccessLayer.Models.AircraftType>, List<AircraftType>>(_unitOfWork.Set<DataAccessLayer.Models.AircraftType>().Get());

        public AircraftType GetDetails(int id) => IsExist(id);

        public void Add(AircraftType aircraftType)
        {
            _unitOfWork.Set<DataAccessLayer.Models.AircraftType>().Create(ConvertToModel(aircraftType));
            _unitOfWork.SaveChages();
        }

        public void Update(AircraftType aircraftType)
        {
            _unitOfWork.Set<DataAccessLayer.Models.AircraftType>().Update(ConvertToModel(aircraftType));
            _unitOfWork.SaveChages();
        }

        public void Remove(int id)
        {
            _unitOfWork.Set<DataAccessLayer.Models.AircraftType>().Delete(id);
            _unitOfWork.SaveChages();
        }

        public void RemoveAll()
        {
            _unitOfWork.Set<DataAccessLayer.Models.AircraftType>().Delete();
            _unitOfWork.SaveChages();
        }
    }
}
