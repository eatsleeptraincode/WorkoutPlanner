using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FubuPersistence;
using Raven.Client;
using WorkoutPlanner.Web.Athletes;
using WorkoutPlanner.Web.Exercises;
using WorkoutPlanner.Web.RepPatterns;

namespace WorkoutPlanner.Web.Workouts
{
    public class PrintWorkoutEndpoint
    {
        private readonly IEntityRepository _repository;
        private readonly IDocumentSession _session;

        public PrintWorkoutEndpoint(IEntityRepository repository, IDocumentSession session)
        {
            _repository = repository;
            _session = session;
        }

        public PrintWorkoutViewModel get_workout_print_WorkoutId(PrintWorkoutViewModel request)
        {
            var workout = _session
                .Include<Workout>(w => w.AthleteId)
                .Include<Workout>(w => w.Exercises.Select(e => e.ExerciseId))
                .Load(request.WorkoutId);
            var athlete = _session.Load<Athlete>(workout.AthleteId);

            var workoutDisplay = new PrintWorkoutDisplay
            {
                AthleteFirstName = athlete.FirstName,
                AthleteLastName = athlete.LastName,
                Phase = workout.Phase,
                Type = workout.Type,
                Exercises = workout.Exercises.Select(exercise => new PrintWorkoutExerciseDisplay
                {
                    Name = _session.Load<Exercise>(exercise.ExerciseId).Name,
                    Down = exercise.Down,
                    Bottom = exercise.Bottom,
                    Up = exercise.Up,
                    Top = exercise.Top,
                    Reps = GetReps(exercise)
                })
            };

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
                var repPattern = _repository.Find<RepPatternCollection>(RepPatternCollection.StaticId).Patterns.Single(p => p.Id == exercise.RepPatternId);
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
        public string AthleteName
        {
            get { return string.Format("{0}, {1}", AthleteLastName, AthleteFirstName); }
        }

        public string AthleteFirstName { get; set; }
        public string AthleteLastName { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
        public IEnumerable<PrintWorkoutExerciseDisplay> Exercises { get; set; }
    }

    public class PrintWorkoutExerciseDisplay
    {
        public string Name { get; set; }
        public int[] Reps { get; set; }
        public int Down { get; set; }
        public int Bottom { get; set; }
        public int Up { get; set; }
        public int Top { get; set; }
        
        public string Tempo {
            get { return string.Format("({0}/{1}/{2}/{3})", Down, Bottom, Up, Top); }
        }

        public int RepCount
        {
            get { return Reps.Length; }
        }

        public string RepDisplay {
            get { return Reps.Select(r => r.ToString(CultureInfo.InvariantCulture)).Join(","); }
        }

    }
}