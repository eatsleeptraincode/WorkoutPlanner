using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.AutoComplete;
using FubuPersistence;
using Raven.Client;
using WorkoutPlanner.Web.Exercises;

namespace WorkoutPlanner.Web.Workouts
{
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
}