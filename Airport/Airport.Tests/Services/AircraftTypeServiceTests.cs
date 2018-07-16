using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FakeItEasy;
using NUnit.Framework;
using DTO = Shared.DTO;

namespace Airport.Tests.Services
{
    [TestFixture]
    public class AircraftTypeServiceTests
    {
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly IRepository<AircraftType> _fakeAircraftTypeRepository;
        private readonly IMapper _fakeMapper;
        private AircraftTypeService _aircraftTypeService;
        private int _aircraftTypeId;
        private AircraftType _type1;
        private DTO.AircraftType _type1DTO;

        public AircraftTypeServiceTests()
        {
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _fakeAircraftTypeRepository = A.Fake<IRepository<AircraftType>>();
            _fakeMapper = A.Fake<IMapper>();
        }

        // This method runs before each test.
        [SetUp]
        public void TestSetup()
        {
            _type1 = new AircraftType() { AircraftModel = "Tupolev Tu-134", SeatsNumber = 80, Carrying = 47000 };
            var plane2 = new AircraftType() { AircraftModel = "Tupolev Tu-204", SeatsNumber = 196, Carrying = 107900 };

            _type1DTO = new DTO.AircraftType() { AircraftModel = "Tupolev Tu-134", SeatsNumber = 80, Carrying = 47000 };

            _aircraftTypeId = 1;

            A.CallTo(() => _fakeMapper.Map<AircraftType, DTO.AircraftType>(_type1)).Returns(_type1DTO);
            A.CallTo(() => _fakeUnitOfWork.Set<AircraftType>()).Returns(_fakeAircraftTypeRepository);
            _aircraftTypeService = new AircraftTypeService(_fakeUnitOfWork, _fakeMapper);
        }

        // This method runs after each test.
        [TearDown]
        public void TestTearDown()
        {
            // _fakeAircraftRepository.Data.Clear();
        }

        [Test]
        public void ValidationForeignId_Should_ReturnTrue_When_Always()
        {
            Assert.IsTrue(_aircraftTypeService.ValidationForeignId(_type1DTO));
        }

        [Test]
        public void IsExists_ShouldReturnAircraftTypeDto_WhenAircraftTypeExists()
        {
            //A.CallTo(() => _fakeAircraftRepository.Get(_aircraftId)).Returns(new List<Aircraft> { _plane1 });
            A.CallTo(() => _fakeUnitOfWork.Set<AircraftType>().Get(_aircraftTypeId)).Returns(new List<AircraftType> { _type1 });
            var result = _aircraftTypeService.IsExist(_aircraftTypeId);
            Assert.AreEqual(_type1DTO, result);
        }

        [Test]
        public void ConvertToModel_Should_ReturnModel_When_Called()
        {
            A.CallTo(() => _fakeMapper.Map<DTO.AircraftType, AircraftType>(_type1DTO)).Returns(_type1);
            var result = _aircraftTypeService.ConvertToModel(_type1DTO);
            Assert.AreEqual(_type1, result);
        }

        [Test]
        public void Add_Should_CallRepositoryCreate_When_Called()
        {
            _aircraftTypeService.Add(_type1DTO);
            A.CallTo(() => _fakeAircraftTypeRepository.Create(A<AircraftType>.That.IsInstanceOf(typeof(AircraftType)), null)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Update_Should_CallRepositoryUpdate_When_Called()
        {
            _aircraftTypeService.Update(_type1DTO);
            A.CallTo(() => _fakeAircraftTypeRepository.Update(A<AircraftType>.That.IsInstanceOf(typeof(AircraftType)), null)).MustHaveHappenedOnceExactly();
        }
    }
}
