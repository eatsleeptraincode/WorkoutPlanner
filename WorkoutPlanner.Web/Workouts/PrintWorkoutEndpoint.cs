using System;
using FubuPersistence;

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