using FubuPersistence;
using FubuTestingSupport;
using NUnit.Framework;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Tests.Web.Workouts
{
    [TestFixture]
    public class CreateWorkoutEndpointTests : InMemoryPersistenceTests
    {
        private readonly CreateWorkoutViewModel _request = new CreateWorkoutViewModel { };
        private CreateWorkoutEndpoint _endpoint;

        [SetUp]
        public void SetUp()
        {
            _endpoint = new CreateWorkoutEndpoint(Container.GetInstance<IEntityRepository>());
        }

        [Test]
        public void GetShouldReturnRequest()
        {
            var result = _endpoint.get_workout_create_AthleteId(_request);
            result.ShouldBeTheSameAs(_request);
        }

        [Test]
        public void PostShouldCreateWorkout()
        {
            Assert.Inconclusive("Not Implemented");
        }
    }
}