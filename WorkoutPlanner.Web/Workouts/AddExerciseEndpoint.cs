using System;
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
            workout.Exercises.Add(new WorkoutExercise
            {
                ExerciseId = request.Exercise,
                Group = request.Group,
                Sets = request.Sets,
                Reps = request.Reps,
                Down = request.Down,
                Bottom = request.Bottom,
                Up = request.Up,
                Top = request.Top,
            });
            return FubuContinuation.RedirectTo(new DetailWorkoutViewModel {WorkoutId = request.WorkoutId});
        }
    }

    public class AddExerciseViewModel : ExerciseViewModel
    {
        public Guid Exercise { get; set; }
    }
}