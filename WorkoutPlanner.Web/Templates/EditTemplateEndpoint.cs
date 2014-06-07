using System;
using System.Collections.Generic;
using System.Linq;
using FubuPersistence;
using Raven.Client;
using WorkoutPlanner.Web.Exercises;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Templates
{
    public class EditTemplateEndpoint
    {
        private readonly IEntityRepository _repository;
        private readonly IDocumentSession _session;

        public EditTemplateEndpoint(IEntityRepository repository, IDocumentSession session)
        {
            _repository = repository;
            _session = session;
        }

        public EditTemplateViewModel get_template_edit_TemplateId(EditTemplateViewModel request)
        {
            //TODO: somewhat duplicated in edit workout
            var workout = _session.Include<WorkoutTemplate>(t => t.Exercises.Select(e => e.ExerciseId)).Load(request.TemplateId);
            var exercises = workout.Exercises.Select(e => new WorkoutExerciseViewModel
            {
                Bottom = e.Bottom,
                Down = e.Down,
                Group = e.Group,
                Reps = e.Reps,
                RepScheme = e.RepScheme,
                RepPatternId = e.RepPatternId,
                Sets = e.Sets,
                Top = e.Top,
                Up = e.Up,
                Exercise = _session.Load<Exercise>(e.ExerciseId).Name,
                ExerciseId = e.ExerciseId
            }).ToList();

            exercises.Add(new WorkoutExerciseViewModel());
            return new EditTemplateViewModel { Phase = workout.Phase, Type = workout.Type, Exercises = exercises, TemplateId = workout.Id };
        }
         
    }

    public class EditTemplateViewModel
    {
        public Guid TemplateId { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
        public IList<WorkoutExerciseViewModel> Exercises { get; set; }
    }
}