using System;
using FubuMVC.Core.Continuations;
using FubuPersistence;
using WorkoutPlanner.Web.Athletes;
using WorkoutPlanner.Web.Templates;

namespace WorkoutPlanner.Web.Workouts
{

    public class CreateWorkoutFromTemplateEndpoint
    {
        private readonly IEntityRepository _repository;

        public CreateWorkoutFromTemplateEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public CreateWorkoutFromTemplateViewModel get_workout_createfromtemplate_AthleteId(
            CreateWorkoutFromTemplateViewModel request)
        {
            
            return request;
        }

        public FubuContinuation post_workout_createfromtemplate(CreateWorkoutFromTemplateViewModel request)
        {
            var template = _repository.Find<WorkoutTemplate>(request.WorkoutTemplateId);
            var workout = new Workout
            {
                AthleteId = request.AthleteId,
                Phase = template.Phase,
                Type = template.Type,
                Exercises = template.Exercises
            };
            _repository.Update(workout);
            return FubuContinuation.RedirectTo(new DetailAthleteViewModel {AthleteId = request.AthleteId});
        }
        
    }

    public class CreateWorkoutFromTemplateViewModel
    {
        public Guid AthleteId { get; set; }
        public Guid WorkoutTemplateId { get; set; }
    }
}