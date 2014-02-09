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
            workout.Exercises.Add(new WorkoutExercise{ExerciseId = request.Exercise, Sets = request.Sets, Reps = request.Reps});
            return FubuContinuation.RedirectTo(new DetailWorkoutViewModel {WorkoutId = request.WorkoutId});
        }
    }

    public class AddExerciseViewModel
    {
        public Guid WorkoutId { get; set; }
        public Guid Exercise { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}