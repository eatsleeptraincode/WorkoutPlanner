using System;
using System.Linq;
using FubuPersistence;
using FubuTestingSupport;
using NUnit.Framework;
using WorkoutPlanner.Web.Athletes;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Tests.Web.Athletes
{
    [TestFixture]
    public class DetailAthleteEndpointTests : InMemoryPersistenceTests
    {
        private const string FirstName = "Ryan";
        private readonly Guid _athleteId = Guid.NewGuid();
        private IEntityRepository _repository;
        private DetailAthleteEndpoint _endpoint;
        private DetailAthleteViewModel _result;

        [SetUp]
        public void SetUp()
        {
            _repository = Container.GetInstance<IEntityRepository>();
            _repository.Update(new Athlete { Id = _athleteId, FirstName = FirstName });
            _repository.Update(new Workout{AthleteId = _athleteId});
            _repository.Update(new Workout{AthleteId = _athleteId});
            _repository.Update(new Workout{AthleteId = Guid.NewGuid()});
            _endpoint = new DetailAthleteEndpoint(_repository);
            _result = _endpoint.get_athlete_detail_AthleteId(new DetailAthleteViewModel { AthleteId = _athleteId });
        }

        [Test]
        public void GetShouldFindAthlete()
        {
            _result.Athlete.ShouldNotBeNull();
            _result.Athlete.FirstName.ShouldEqual(FirstName);
        }

        [Test]
        public void GetShouldFindAthletesWorkouts()
        {
            _result.Workouts.Count().ShouldEqual(2);
        }
    }
}