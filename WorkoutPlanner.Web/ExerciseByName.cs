using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using WorkoutPlanner.Web.Exercises;

namespace WorkoutPlanner.Web
{
    public class ExerciseByName : AbstractIndexCreationTask<Exercise>
    {
        public ExerciseByName()
        {
            Map = exercises => from e in exercises
                select new {e.Name};
            Indexes.Add(e => e.Name, FieldIndexing.Analyzed);
        }
    }
}