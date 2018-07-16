using System;
using System.Collections.Generic;
using System.Text;
using Airport.Tests.Repository;
using AutoMapper;
using BusinessLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using FakeItEasy;
using NUnit.Framework;
using DTO = Shared.DTO;

namespace Airport.Tests.Services
{
    [TestFixture]
    public class AircraftServiceTests
    {
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly IRepository<Aircraft> _fakeAircraftRepository;
        private IMapper _fakeMapper;
        private AircraftService _aircraftService;
        private int _aircraftId;
        private Aircraft plane1;
        private Aircraft plane2;

        public AircraftServiceTests()
        {
            _fakeAircraftRepository = A.Fake<IRepository<Aircraft>>();
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _fakeMapper = A.Fake<IMapper>();
        }

        // This method runs before each test.
        [SetUp]
        public void TestSetup()
        {
            var plane1 = new Aircraft()
            {
                AircraftName = "Strong",
                AircraftType = new AircraftType() { AircraftModel = "Tupolev Tu-134", SeatsNumber = 80, Carrying = 47000 },
                AircraftReleaseDate = new DateTime(2011, 6, 10),
                ExploitationTimeSpan = new DateTime(2021, 6, 10) - new DateTime(2011, 6, 10)
            };
            var plane2 = new Aircraft()
            {
                AircraftName = "Dog",
                AircraftType = new AircraftType() { AircraftModel = "Tupolev Tu-204", SeatsNumber = 196, Carrying = 107900 },
                AircraftReleaseDate = new DateTime(2007, 6, 10),
                ExploitationTimeSpan = new DateTime(2020, 6, 10) - new DateTime(2011, 6, 10)
            };

            var plane1DTO = new DTO.Aircraft
            {
                Id = _aircraftId,
                AircraftName = "Strong",
                AircraftType = new DTO.AircraftType() { AircraftModel = "Tupolev Tu-134", SeatsNumber = 80, Carrying = 47000 },
                AircraftReleaseDate = new DateTime(2011, 6, 10),
                ExploitationTimeSpan = new DateTime(2021, 6, 10) - new DateTime(2011, 6, 10)
            };

            _aircraftId = 1;
            
            A.CallTo(() => _fakeMapper.Map<Aircraft, Shared.DTO.Aircraft>(plane1)).Returns(plane1DTO);

            _aircraftService = new AircraftService(_fakeUnitOfWork, _fakeMapper);
        }

        // This method runs after each test.
        [TearDown]
        public void TestTearDown()
        {
           // _fakeAircraftRepository.Data.Clear();
        }

        [Test]
        public void IsExists_ShouldReturnAircraft_WhenAircraftExistsInRepository()
        {
            A.CallTo(() => _fakeAircraftRepository.Get(_aircraftId)).Returns(new List<Aircraft> { plane1 });

            A.CallTo(() => _fakeUnitOfWork.AircraftRepository.Get(_aircraftId)).Returns(new List<Aircraft> { plane1 });

            //A.CallTo(() => _fakeUnitOfWork.AircraftRepository).Returns(_fakeAircraftRepository);

            var result = _aircraftService.IsExist(_aircraftId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_Should_CallRepositoryCreate_When_Called()
        {
            A.CallTo(() => _fakeAircraftRepository.Create(A<Aircraft>.That.IsEqualTo(plane1), null)).MustHaveHappenedOnceExactly();
            Assert.IsNotNull(result);
        }
    }
}
