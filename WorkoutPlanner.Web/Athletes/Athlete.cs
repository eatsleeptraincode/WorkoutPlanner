using System;
using System.Collections.Generic;
using FubuPersistence;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Athletes
{
    public class Athlete : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public IList<Workout> Workouts { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public int Age
        {
            get
            {
                DateTime today = CurrentDate.Today();
                int age = today.Year - BirthDate.Year;
                if (BirthDate > today.AddYears(-age)) age--;
                return age;
            }
        }

        public Athlete()
        {
            Workouts = new List<Workout>();
        }
    }
}