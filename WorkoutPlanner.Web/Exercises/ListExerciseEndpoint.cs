using System.Collections.Generic;
using FubuPersistence;

namespace WorkoutPlanner.Web.Exercises
{
    public class ListExerciseEndpoint
    {
        private readonly IEntityRepository _repository;

        public ListExerciseEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public ListExerciseViewModel get_exercise_list(ListExerciseViewModel request)
        {
            var exercises = _repository.All<Exercise>();
            return new ListExerciseViewModel { Exercises = exercises };
        }
    }

    public class ListExerciseViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}