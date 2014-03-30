using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FubuPersistence;
using WorkoutPlanner.Web.Athletes;
using WorkoutPlanner.Web.Exercises;
using WorkoutPlanner.Web.RepPatterns;

namespace WorkoutPlanner.Web.Workouts
{
    public class PrintWorkoutEndpoint
    {
        private readonly IEntityRepository _repository;

        public PrintWorkoutEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public PrintWorkoutViewModel get_workout_print_WorkoutId(PrintWorkoutViewModel request)
        {
            var workout = _repository.Find<Workout>(request.WorkoutId);
            var athlete = _repository.Find<Athlete>(workout.AthleteId);

            var workoutDisplay = new PrintWorkoutDisplay
            {
                AthleteName = athlete.LastName + ", " + athlete.FirstName,
                Phase = workout.Phase,
                Type = workout.Type
            };

            foreach (var exercise in workout.Exercises)
            {
                var reps = GetReps(exercise);
                workoutDisplay.Exercises.Add(new PrintWorkoutExerciseDisplay
                {
                    Name = _repository.Find<Exercise>(exercise.ExerciseId).Name,
                    Tempo = string.Format("({0}/{1}/{2}/{3})", exercise.Down, exercise.Bottom, exercise.Up, exercise.Top),
                    RepCount = reps.Length,
                    RepDisplay = reps.Select(r => r.ToString(CultureInfo.InvariantCulture)).Join(",")
                });
            }

            request.Workout = workoutDisplay;
            return request;
        }

        private int[] GetReps(WorkoutExercise exercise)
        {
            if (exercise.RepScheme == RepScheme.StraightSets)
            {
                var repCount = new int[exercise.Sets];
                for (int i = 0; i < repCount.Length; i++)
                {
                    repCount[i] = exercise.Reps;
                }
                return repCount;
            }
            else
            {
                var repPattern = _repository.All<RepPatternCollection>().First().Patterns.Single(p => p.Id == exercise.RepPatternId);
                return repPattern.Pattern.Select(p => p.Amount).ToArray();
            }
        }
    }

    public class PrintWorkoutViewModel
    {
        public Guid WorkoutId { get; set; }
        public PrintWorkoutDisplay Workout { get; set; }
    }

    public class PrintWorkoutDisplay
    {
        public string AthleteName { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
        public IList<PrintWorkoutExerciseDisplay> Exercises { get; set; }

        public PrintWorkoutDisplay()
        {
            Exercises = new List<PrintWorkoutExerciseDisplay>();
        }
    }

    public class PrintWorkoutExerciseDisplay
    {
        public string Name { get; set; }
        public string Tempo { get; set; }
        public int RepCount { get; set; }
        public string RepDisplay { get; set; }

    }
}