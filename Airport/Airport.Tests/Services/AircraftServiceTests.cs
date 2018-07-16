using System;
using System.Collections.Generic;
using System.Text;
using Airport.Tests.Repository;
using BusinessLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using FakeItEasy;
using NUnit.Framework;
using StackExchange.Redis;

namespace Airport.Tests.Services
{
    [TestFixture]
    public class AircraftServiceTests
    {
        private readonly FakeUnitOfWork _fakeUnitOfWork;
        private readonly FakeRepository<Aircraft> _fakeAircraftRepository;
        private readonly AircraftService _arAircraftService;

        public AircraftServiceTests()
        {
            _fakeAircraftRepository = new FakeRepository<Aircraft>();
            var fakeUnitOfWork = A.Fake<IUnitOfWork>(x => x.ConfigureFake(z => z.AircraftRepository = _fakeAircraftRepository));
            A.CallTo(() => fakeUnitOfWork.AircraftRepository).Returns(_fakeAircraftRepository);
           // fakeUnitOfWork.AircraftRepository = new FakeRepository<Aircraft>();
            

            //_fakeUnitOfWork = new FakeUnitOfWork(


            //        uow.SetRepository(_fakeAircraftRepository);

            //    );
            _arAircraftService = new AircraftService(fakeUnitOfWork);
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
            var plane3 = new Aircraft()
            {
                AircraftName = "Sky",
                AircraftType = new AircraftType() { AircraftModel = "Ilyushin IL-62", SeatsNumber = 138, Carrying = 280300 },
                AircraftReleaseDate = new DateTime(2015, 6, 10),
                ExploitationTimeSpan = new DateTime(2027, 6, 10) - new DateTime(2011, 6, 10)
            };

            _fakeAircraftRepository.Data.AddRange(new[] { plane1, plane2, plane3 });
        }

        // This method runs after each test.
        [TearDown]
        public void TestTearDown()
        {
            _fakeAircraftRepository.Data.Clear();
        }

        [Test]
        public void IsExists_ShouldReturnAircraft_WhenAircraftExistsInRepository()
        {
            const int id = 1;
            var result = _arAircraftService.IsExist(id);
            Assert.IsNotNull(result);
        }
    }
}
