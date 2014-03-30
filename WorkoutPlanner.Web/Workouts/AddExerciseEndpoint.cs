using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuPersistence;

namespace WorkoutPlanner.Web.Workouts
{
    public class AddExerciseEndpoint
    {
        private readonly IEntityRepository _repository;

        public AddExerciseEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public FubuContinuation post_workout_addexercise(AddExerciseViewModel request)
        {
            var workout = _repository.Find<Workout>(request.WorkoutId);
            workout.Exercises = request.Exercises.Where(e => e.ExerciseId != Guid.Empty).Select(e => new WorkoutExercise
            {
                Bottom = e.Bottom,
                Down = e.Down,
                ExerciseId = e.ExerciseId,
                Group = e.Group,
                Reps = e.Reps,
                RepScheme = e.RepScheme,
                RepPatternId = e.RepPatternId,
                Sets = e.Sets,
                Top = e.Top,
                Up = e.Up
            }).ToList();
            return FubuContinuation.RedirectTo(new DetailWorkoutViewModel {WorkoutId = request.WorkoutId});
        }
    }

    public class AddExerciseViewModel : ExerciseViewModel
    {
        public Guid WorkoutId { get; set; }
        public IList<AddWorkoutExerciseViewModel> Exercises { get; set; }
    }

    public class AddWorkoutExerciseViewModel
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
        public RepScheme RepScheme { get; set; }
        public Guid RepPatternId { get; set; }
    }
}