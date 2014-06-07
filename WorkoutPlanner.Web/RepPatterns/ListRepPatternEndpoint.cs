using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FubuPersistence;

namespace WorkoutPlanner.Web.RepPatterns
{
    public class ListRepPatternEndpoint
    {
        private readonly IEntityRepository _repository;

        public ListRepPatternEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public ListRepPatternViewModel get_reppatterns_list(ListRepPatternViewModel request)
        {
            var patterns = _repository.Find<RepPatternCollection>(RepPatternCollection.StaticId);

            var list = patterns.Patterns.Select(pattern => new KeyValuePair<Guid, string>(pattern.Id, pattern.Pattern
                .Select(p => p.Amount.ToString(CultureInfo.InvariantCulture))
                .ToArray().Join(","))).ToList();
            request.Patterns = list;
            return request;
        }
    }

    public class ListRepPatternViewModel
    {
        public IEnumerable<KeyValuePair<Guid,String>> Patterns { get; set; }
    }

    public class RepPatternCollection : Entity
    {
        public static readonly Guid StaticId = new Guid("10476d09-c226-4d2a-9e5b-5da3b38b8f3a");
        public IList<RepPattern> Patterns { get; set; }
    }

    public class RepPattern : Entity
    {
        public Rep[] Pattern { get; set; }
    }

    public class Rep
    {
        public int Amount { get; set; }
    }
}