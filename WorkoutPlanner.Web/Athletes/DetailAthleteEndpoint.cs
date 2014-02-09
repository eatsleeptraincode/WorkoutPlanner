using System;
using System.Collections.Generic;
using System.Linq;
using FubuPersistence;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Athletes
{
    public class DetailAthleteEndpoint
    {
        private readonly IEntityRepository _repository;

        public DetailAthleteEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public DetailAthleteViewModel get_athlete_detail_AthleteId(DetailAthleteViewModel request)
        {
            var athlete = _repository.Find<Athlete>(request.AthleteId);
            var workouts = _repository.All<Workout>().Where(w => w.AthleteId == request.AthleteId);
            return new DetailAthleteViewModel{Athlete = athlete, Workouts = workouts};
        }
    }

    public class DetailAthleteViewModel
    {
        public Guid AthleteId { get; set; }
        public Athlete Athlete { get; set; }
        public IEnumerable<Workout> Workouts { get; set; }
    }
}