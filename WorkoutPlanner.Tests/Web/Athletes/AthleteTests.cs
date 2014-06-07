using System;
using FubuTestingSupport;
using NUnit.Framework;
using WorkoutPlanner.Web.Athletes;

namespace WorkoutPlanner.Tests.Web.Athletes
{
    [TestFixture]
    public class AthleteTests
    {
        [Test]
        public void Age_WhenDateBeforeBirthday_ShouldBeLessThanYears()
        {
            var athlete = new Athlete{BirthDate = new DateTime(1980,8,18)};
            CurrentDate.Today = () => new DateTime(2014, 4, 18);
            athlete.Age.ShouldEqual(33);
        }

        [Test]
        public void Age_WhenDateAfterBirthday_ShouldBeEqualToYears()
        {
            var athlete = new Athlete { BirthDate = new DateTime(1980, 8, 18) };
            CurrentDate.Today = () => new DateTime(2014, 9, 18);
            athlete.Age.ShouldEqual(34);
        }
    }
}