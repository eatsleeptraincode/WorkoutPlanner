using System;
using System.Collections.Generic;
using System.Linq;
using FubuPersistence;
using WorkoutPlanner.Web.Exercises;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Templates
{
    public class EditTemplateEndpoint
    {
        private readonly IEntityRepository _repository;

        public EditTemplateEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public EditTemplateViewModel get_template_edit_TemplateId(EditTemplateViewModel request)
        {
            var workout = _repository.Find<WorkoutTemplate>(request.TemplateId);
            //TODO: Make less horrible
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
                Exercise = _repository.Find<Exercise>(e.ExerciseId).Name,
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