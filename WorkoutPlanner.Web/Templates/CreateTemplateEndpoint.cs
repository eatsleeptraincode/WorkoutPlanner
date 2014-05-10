using System;
using System.Collections.Generic;
using FubuMVC.Core.Continuations;
using FubuMVC.Spark.SparkModel;
using FubuPersistence;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Templates
{
    public class CreateTemplateEndpoint
    {
        private readonly IEntityRepository _repository;

        public CreateTemplateEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public CreateTemplateViewModel get_createtemplate(CreateTemplateViewModel request)
        {
            return request;
        }

        public FubuContinuation post_createtemplate(CreateTemplateViewModel request)
        {
            _repository.Update(new WorkoutTemplate{Type = request.Type, Phase = request.Phase});
            return FubuContinuation.RedirectTo(new ListTemplateViewModel());
        }
    }

    public class WorkoutTemplate : Entity
    {
        public string Type { get; set; }
        public int Phase { get; set; }

        public Guid AthleteId { get; set; }
        public IList<WorkoutExercise> Exercises { get; set; }

        public WorkoutTemplate()
        {
            Exercises = new List<WorkoutExercise>();
        }
    }

    public class CreateTemplateViewModel    
    {
        public string Type { get; set; }
        public int Phase { get; set; }
    }
}