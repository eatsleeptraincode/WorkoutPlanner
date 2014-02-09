using System;
using System.Collections.Generic;
using FubuPersistence;

namespace WorkoutPlanner.Web.Workouts
{
    public class Workout : Entity
    {
        public Guid AthleteId { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
        public IList<WorkoutExercise> Exercises { get; set; }

        public Workout()
        {
            Exercises = new List<WorkoutExercise>();
        }
    }

    public class WorkoutExercise
    {
        public Guid ExerciseId { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }

    public class WorkoutType
    {
    }
}