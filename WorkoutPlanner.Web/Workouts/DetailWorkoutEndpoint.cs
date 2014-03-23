using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.AutoComplete;
using FubuMVC.Core.Registration;
using FubuPersistence;
using Raven.Client;
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


            var exercises = workout.Exercises.Select(e => new WorkoutExerciseViewModel
            {
                Bottom = e.Bottom,
                Down = e.Down,
                Group = e.Group,
                Reps = e.Reps,
                Sets = e.Sets,
                Top = e.Top,
                Up = e.Up,
                Exercise = _repository.Find<Exercise>(e.ExerciseId).Name,
                ExerciseId = e.ExerciseId
            }).ToList();

            exercises.Add(new WorkoutExerciseViewModel());
            return new DetailWorkoutViewModel {Phase = workout.Phase, Type = workout.Type,Exercises = exercises, WorkoutId = workout.Id};
        }
    }

    public class ExerciseLookup : ILookupProvider
    {
        private readonly IEntityRepository _repository;
        private readonly IDocumentSession _session;

        public ExerciseLookup(IEntityRepository repository, IDocumentSession session)
        {
            _repository = repository;
            _session = session;
        }

        public object IdFor(string label)
        {
            var exercise = _repository.FindWhere<Exercise>(e => e.Name == label);
            return exercise.Id;
        }

        public string LabelFor(object value)
        {
            var id = Guid.Parse(value.ToString());
            var exercise = _repository.Find<Exercise>(id);
            return exercise.Name;
        }

        public IEnumerable<LookupItem> Lookup(AutoCompleteQuery query)
        {
            var exercises = _session.Query<Exercise, ExerciseByName>()
                .Search(e => e.Name,
                    string.Format("*{0}*", query.term),
                    escapeQueryOptions: EscapeQueryOptions.AllowAllWildcards,
                    options: SearchOptions.And)
                .ToList();

            return exercises.Select(e => new LookupItem
            {
                label = e.Name,
                value = e.Id.ToString()
            });
        }
    }

    public class DetailWorkoutViewModel
    {
        public Guid WorkoutId { get; set; }
        public string Type { get; set; }
        public int Phase { get; set; }
        public IList<WorkoutExerciseViewModel> Exercises { get; set; }
    }

    public class WorkoutExerciseViewModel
    {
        public ExerciseGroup Group { get; set; }
        [AutoComplete(typeof(ExerciseLookup))]
        public string Exercise { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Down { get; set; }
        public int Bottom { get; set; }
        public int Up { get; set; }
        public int Top { get; set; }
        public Guid ExerciseId { get; set; }
    }


    public class ExerciseViewModel
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public ExerciseGroup Group { get; set; }

        public int Down { get; set; }
        public int Bottom { get; set; }
        public int Up { get; set; }
        public int Top { get; set; }
    }

}