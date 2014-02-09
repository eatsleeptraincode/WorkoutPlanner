using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.AutoComplete;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuPersistence;
using WorkoutPlanner.Web.Exercises;

namespace WorkoutPlanner.Web.Workouts
{
    public class DetailWorkoutEndpoint
    {
        private readonly IEntityRepository _repository;

        public DetailWorkoutEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public DetailWorkoutViewModel get_workout_detail_WorkoutId(DetailWorkoutViewModel request)
        {
            var workout = _repository.Find<Workout>(request.WorkoutId);
            //TODO: Make less horrible
            foreach (var workoutExercise in workout.Exercises)
            {
                var exercise = _repository.Find<Exercise>(workoutExercise.ExerciseId);
                workoutExercise.Name = exercise.Name;
            }
            return new DetailWorkoutViewModel {Workout = workout, WorkoutId = workout.Id};
        }
    }

    public class DetailWorkoutOverrides : OverridesFor<DetailWorkoutViewModel>
    {
        public DetailWorkoutOverrides()
        {
            Property(m => m.Exercise).AutoComplete<ExerciseLookup>();
        }
    }

    public class ExerciseLookup : ILookupProvider
    {
        private readonly IEntityRepository _repository;

        public ExerciseLookup(IEntityRepository repository)
        {
            _repository = repository;
        }

        public object IdFor(string label)
        {
            var exercise = _repository.FindWhere<Exercise>(e => e.Name == label);
            return exercise.Id;
        }

        public string LabelFor(object value)
        {
            var id = Guid.Parse(value.ToString());
            var exercise = _repository.FindWhere<Exercise>(e => e.Id == id);
            return exercise.Name;
        }

        public IEnumerable<LookupItem> Lookup(AutoCompleteQuery query)
        {
            return
                _repository.All<Exercise>()
                    .Where(e => e.Name.StartsWith(query.term)).ToList()
                    .Select(e => new LookupItem { label = e.Name, value = e.Id.ToString() });
        }
    }

    public class DetailWorkoutViewModel
    {
        public Guid WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
        public ExerciseGroup Group { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}