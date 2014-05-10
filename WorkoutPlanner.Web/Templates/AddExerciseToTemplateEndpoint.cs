using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuPersistence;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Templates
{
    public class AddExerciseToTemplateEndpoint
    {
        private readonly IEntityRepository _repository;

        public AddExerciseToTemplateEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public FubuContinuation post_workout_addexercise(AddExerciseToTemplateViewModel request)
        {
            var workout = _repository.Find<WorkoutTemplate>(request.TemplateId);
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
            return FubuContinuation.RedirectTo(new EditTemplateViewModel {TemplateId = request.TemplateId});
        }
    }

    public class AddExerciseToTemplateViewModel : ExerciseViewModel
    {
        public Guid TemplateId { get; set; }
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