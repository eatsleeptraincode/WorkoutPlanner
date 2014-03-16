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
        public ExerciseGroup Group { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Down { get; set; }
        public int Bottom { get; set; }
        public int Up { get; set; }
        public int Top { get; set; }
    }

    public enum ExerciseGroup
    {
        A,
        A1,
        A2,
        A3,
        B,
        B1,
        B2,
        B3,
        C,
        C1,
        C2,
        C3,
        D,
        D1,
        D2,
        D3,
        E,
        E1,
        E2,
        E3,
    }

    public class WorkoutType
    {
    }
}