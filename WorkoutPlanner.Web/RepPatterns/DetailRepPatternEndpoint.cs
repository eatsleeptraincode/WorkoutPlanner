using System;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuPersistence;

namespace WorkoutPlanner.Web.RepPatterns
{
    public class DetailRepPatternEndpoint
    {
        private readonly IEntityRepository _repository;

        public DetailRepPatternEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public DetailRepPatternViewModel get_reppattern_detail_RepPatternId(DetailRepPatternViewModel request)
        {
            var repPattern = _repository.All<RepPatternCollection>().First().Patterns.SingleOrDefault(p => p.Id == request.RepPatternId);
            if (repPattern == null)
            {
                request.RepPatternId = Guid.NewGuid();
                request.Pattern = new RepPattern{ Pattern = new Rep[0]};
            }
            else
            {
                request.Pattern = repPattern;
            }
            return request;
        }

        public FubuContinuation post_reppattern_detail(DetailRepPatternViewModel request)
        {
            var patternCollection = _repository.All<RepPatternCollection>()
                .First();
            var pattern =
                patternCollection
                    .Patterns.SingleOrDefault(p => p.Id == request.RepPatternId);
            if (pattern == null)
            {
                patternCollection.Patterns.Add(new RepPattern{Id = request.RepPatternId, Pattern = request.Pattern.Pattern});
            }
            else
            {
                pattern.Pattern = request.Pattern.Pattern;
            }
            return FubuContinuation.RedirectTo(new DetailRepPatternViewModel {RepPatternId = request.RepPatternId});
        }
    }

    public class DetailRepPatternViewModel
    {
        public Guid RepPatternId { get; set; }
        public RepPattern Pattern { get; set; }
    }
}