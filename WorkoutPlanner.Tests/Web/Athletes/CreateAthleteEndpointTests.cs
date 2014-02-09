using System;
using FubuPersistence;
using FubuTestingSupport;
using NUnit.Framework;
using WorkoutPlanner.Web.Athletes;

namespace WorkoutPlanner.Tests.Web.Athletes
{
    [TestFixture]
    public class CreateAthleteEndpointTests : InMemoryPersistenceTests
    {
        private const string FirstName = "Ryan";
        private const string LastName = "Weppler";
        private const string PhoneNumber = "(204)123-4567";
        private CreateAthleteEndpoint _endpoint;
        private CreateAthleteViewModel _request;
        private readonly DateTime _birthDate = DateTime.Now;
        private const string Email = "ryan@example.org";

        [SetUp]
        public void Setup()
        {
            _endpoint = new CreateAthleteEndpoint(Container.GetInstance<IEntityRepository>());
            _request = new CreateAthleteViewModel
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                Email = Email,
                BirthDate = _birthDate
            };
        }

        [Test]
        public void GetShouldReturnRequest()
        {
            var result = _endpoint.get_athlete_create(_request);
            result.ShouldBeTheSameAs(_request);
        }

        [Test]
        public void PostShouldUpdateAthlete()
        {
            _endpoint.post_athlete_create(_request);
            var athlete = Container.GetInstance<IEntityRepository>()
                .FindWhere<Athlete>(
                    a =>
                        a.FirstName == FirstName
                        && a.LastName == LastName
                        && a.PhoneNumber == PhoneNumber
                        && a.Email == Email
                        && a.BirthDate == _birthDate);
            athlete.ShouldNotBeNull();
        }

        [Test]
        public void PostShouldRedirectToAthleteList()
        {
            var result = _endpoint.post_athlete_create(_request);
            result.AssertWasRedirectedTo<ListAthleteViewModel>(m => true);
        }
    }
}