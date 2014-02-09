using System.Collections.Generic;
using FubuPersistence;

namespace WorkoutPlanner.Web.Athletes
{
    public class ListAthleteEndpoint
    {
        private readonly IEntityRepository _repository;

        public ListAthleteEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public ListAthleteViewModel get_athlete_list(ListAthleteViewModel request)
        {
            var athletes = _repository.All<Athlete>();
            return new ListAthleteViewModel {Athletes = athletes};
        }
    }

    public class ListAthleteViewModel
    {
        public IEnumerable<Athlete> Athletes { get; set; }
    }
}