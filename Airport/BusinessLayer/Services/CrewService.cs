using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Shared.DTO;

namespace BusinessLayer.Services
{
    public class CrewService : IService<Crew>
    {
        private readonly UnitOfWork _unitOfWork;

        public CrewService(AirportContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public bool ValidationForeignId(Crew ob)
        {
            if (ob.Stewardesses == null) return false;
            foreach (var st in ob.Stewardesses)
            {
                if (_unitOfWork.Set<DataAccessLayer.Models.Stewardess>().Get(st.Id).FirstOrDefault() == null) return false;
            }
            return _unitOfWork.Set<DataAccessLayer.Models.Pilot>().Get().FirstOrDefault(o => o.Id == ob.Pilot.Id) != null;
            
        }

        public Crew IsExist(int id) => Mapper.Map<DataAccessLayer.Models.Crew, Crew>(_unitOfWork.CrewRepository.Get(id).FirstOrDefault());

        public DataAccessLayer.Models.Crew ConvertToModel(Crew crew) => Mapper.Map<Crew, DataAccessLayer.Models.Crew>(crew);

        public List<Crew> GetAll() => Mapper.Map<List<DataAccessLayer.Models.Crew>, List<Crew>>(_unitOfWork.CrewRepository.Get());

        public Crew GetDetails(int id) => IsExist(id);

        public void Add(Crew crew)
        {
            _unitOfWork.CrewRepository.Create(ConvertToModel(crew));
            _unitOfWork.SaveChages();
        }

        public void Update(Crew crew)
        {
            _unitOfWork.CrewRepository.Update(ConvertToModel(crew));
            _unitOfWork.SaveChages();
        }

        public void Remove(int id)
        {
            _unitOfWork.CrewRepository.Delete(id);
            _unitOfWork.SaveChages();
        }

        public void RemoveAll()
        {
            _unitOfWork.CrewRepository.Delete();
            _unitOfWork.SaveChages();
        }
    }
}
