using System;
using FubuPersistence;
using WorkoutPlanner.Web.Exercises;

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
            foreach (var exercise in workout.Exercises)
            {
                exercise.Name = _repository.Find<Exercise>(exercise.ExerciseId).Name;
            }
            request.Workout = workout;
            return request;
        }
    }

    public class PrintWorkoutViewModel
    {
        public Guid WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}