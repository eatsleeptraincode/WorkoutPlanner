using System;
using FubuMVC.Core.Continuations;
using FubuPersistence;
using WorkoutPlanner.Web.Athletes;

namespace WorkoutPlanner.Web.Workouts
{
    public class CreateWorkoutEndpoint
    {
        private readonly IEntityRepository _repository;

        public CreateWorkoutEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public CreateWorkoutViewModel get_workout_create_AthleteId(CreateWorkoutViewModel request)
        {
            return request;
        }

        public FubuContinuation post_workout_create(CreateWorkoutViewModel request)
        {
            _repository.Update(new Workout
            {
                Id = Guid.NewGuid(),
                AthleteId = request.AthleteId,
                Type = request.Type,
                Phase = request.Phase
            });
            return FubuContinuation.RedirectTo(new DetailAthleteViewModel {AthleteId = request.AthleteId});
        }
    }

    public class CreateWorkoutViewModel 
    {
        public Guid AthleteId { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
    }
}