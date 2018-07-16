using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CrewServiceTests
    {
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly IRepository<Crew> _fakeCrewRepository;
        private readonly IRepository<Pilot> _fakePilotRepository;
        private readonly IRepository<Stewardess> _fakeStewardessRepository;
        private readonly IMapper _fakeMapper;
        private CrewService _crewService;
        private int _crewId;
        private Crew _crew1;
        private DTO.Crew _crew1DTO;

        public CrewServiceTests()
        {
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _fakeCrewRepository = A.Fake<IRepository<Crew>>();
            _fakePilotRepository = A.Fake<IRepository<Pilot>>();
            _fakeStewardessRepository = A.Fake<IRepository<Stewardess>>();
            _fakeMapper = A.Fake<IMapper>();
        }

        // This method runs before each test.
        [SetUp]
        public void TestSetup()
        {
            _crew1 = new Crew()
            {
                Pilot = new Pilot() { FirstName = "Adam", LastName = "Black", Dob = new DateTime(1978, 03, 03), Experience = 9 },
                Stewardesses = new List<Stewardess> { new Stewardess() { CrewId = 1, FirstName = "Anna", LastName = "Black", Dob = new DateTime(1993, 02, 03) } }
            };
            var _crew2 = new Crew()
            {
                Pilot = new Pilot() { FirstName = "John", LastName = "Smith", Dob = new DateTime(1983, 07, 11), Experience = 5 },
                Stewardesses = new List<Stewardess> { new Stewardess() { CrewId = 2, FirstName = "Anna", LastName = "Red", Dob = new DateTime(1991, 01, 07) } }
            };

            _crew1DTO = new DTO.Crew()
            {
                Pilot = new DTO.Pilot() { FirstName = "Adam", LastName = "Black", Dob = new DateTime(1978, 03, 03), Experience = 9 },
                Stewardesses = new List<DTO.Stewardess> { new DTO.Stewardess() { CrewId = 1, FirstName = "Anna", LastName = "Black", Dob = new DateTime(1993, 02, 03) } }
            };

            _crewId = 1;

            A.CallTo(() => _fakeMapper.Map<Crew, DTO.Crew>(_crew1)).Returns(_crew1DTO);
            A.CallTo(() => _fakeUnitOfWork.CrewRepository).Returns(_fakeCrewRepository);
            A.CallTo(() => _fakeUnitOfWork.Set<Pilot>()).Returns(_fakePilotRepository);
            A.CallTo(() => _fakeUnitOfWork.Set<Stewardess>()).Returns(_fakeStewardessRepository);
            _crewService = new CrewService(_fakeUnitOfWork, _fakeMapper);
        }

        // This method runs after each test.
        [TearDown]
        public void TestTearDown()
        {
            // _fakeAircraftRepository.Data.Clear();
        }

        [Test]
        public void ValidationForeignId_Should_ReturnTrue_When_PilotAndListStewardessesExist()
        {
            A.CallTo(() => _fakeUnitOfWork.Set<Pilot>().Get(null)).Returns(new List<Pilot> { _crew1.Pilot });
            A.CallTo(() => _fakeUnitOfWork.Set<Stewardess>().Get(null)).Returns(new List<Stewardess> { _crew1.Stewardesses.First() });
            var result = _crewService.ValidationForeignId(_crew1DTO);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidationForeignId_Should_ReturnFalse_When_PilotAndListStewardessesDontExist()
        {
            A.CallTo(() => _fakeUnitOfWork.Set<Pilot>().Get(null)).Returns(null);
            A.CallTo(() => _fakeUnitOfWork.Set<Stewardess>().Get(null)).Returns(null);
            var result = _crewService.ValidationForeignId(_crew1DTO);
            Assert.IsFalse(result);
        }

        [Test]
        public void IsExists_ShouldReturnCrewDto_WhenCrewExists()
        {
            //A.CallTo(() => _fakeAircraftRepository.Get(_crewId)).Returns(new List<Aircraft> { _plane1 });
            A.CallTo(() => _fakeUnitOfWork.CrewRepository.Get(_crewId)).Returns(new List<Crew> { _crew1 });
            var result = _crewService.IsExist(_crewId);
            Assert.AreEqual(_crew1DTO, result);
        }

        [Test]
        public void ConvertToModel_Should_ReturnModel_When_Called()
        {
            A.CallTo(() => _fakeMapper.Map<DTO.Crew, Crew>(_crew1DTO)).Returns(_crew1);
            var result = _crewService.ConvertToModel(_crew1DTO);
            Assert.AreEqual(_crew1, result);
        }

        [Test]
        public void Add_Should_CallRepositoryCreate_When_Called()
        {
            _crewService.Add(_crew1DTO);
            A.CallTo(() => _fakeCrewRepository.Create(A<Crew>.That.IsInstanceOf(typeof(Crew)), null)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Update_Should_CallRepositoryUpdate_When_Called()
        {
            _crewService.Update(_crew1DTO);
            A.CallTo(() => _fakeCrewRepository.Update(A<Crew>.That.IsInstanceOf(typeof(Crew)), null)).MustHaveHappenedOnceExactly();
        }
    }
}
