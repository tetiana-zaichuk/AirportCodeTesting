using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Shared.DTO;

namespace BusinessLayer.Services
{
    public class PilotService : IService<Pilot>
    {
        private readonly UnitOfWork _unitOfWork;

        public PilotService(AirportContext context) => _unitOfWork = new UnitOfWork(context);

        public bool ValidationForeignId(Pilot ob) => true;

        public Pilot IsExist(int id)
            => Mapper.Map<DataAccessLayer.Models.Pilot, Pilot>(_unitOfWork.Set<DataAccessLayer.Models.Pilot>().Get(id).FirstOrDefault());

        public DataAccessLayer.Models.Pilot ConvertToModel(Pilot pilot)
            => Mapper.Map<Pilot, DataAccessLayer.Models.Pilot>(pilot);

        public List<Pilot> GetAll()
            => Mapper.Map<List<DataAccessLayer.Models.Pilot>, List<Pilot>>(_unitOfWork.Set<DataAccessLayer.Models.Pilot>().Get());

        public Pilot GetDetails(int id) => IsExist(id);

        public void Add(Pilot pilot)
        {
            _unitOfWork.Set<DataAccessLayer.Models.Pilot>().Create(ConvertToModel(pilot));
            _unitOfWork.SaveChages();
        }

        public void Update(Pilot pilot)
        {
            _unitOfWork.Set<DataAccessLayer.Models.Pilot>().Update(ConvertToModel(pilot));
            _unitOfWork.SaveChages();
        }

        public void Remove(int id)
        {
            _unitOfWork.Set<DataAccessLayer.Models.Pilot>().Delete(id);
            _unitOfWork.SaveChages();
        }

        public void RemoveAll()
        {
            _unitOfWork.Set<DataAccessLayer.Models.Pilot>().Delete();
            _unitOfWork.SaveChages();
        }
    }
}
