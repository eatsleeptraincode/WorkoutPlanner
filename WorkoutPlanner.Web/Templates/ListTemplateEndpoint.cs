using System.Collections.Generic;
using FubuPersistence;

namespace WorkoutPlanner.Web.Templates
{
    public class ListTemplateEndpoint
    {
        private readonly IEntityRepository _repository;

        public ListTemplateEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public ListTemplateViewModel get_template_list(ListTemplateViewModel request)
        {
            request.Templates = _repository.All<WorkoutTemplate>();
            request.Macros = _repository.All<Macro>();
            return request;
        }
    }

    public class ListTemplateViewModel
    {
        public IEnumerable<WorkoutTemplate> Templates { get; set; }
        public IEnumerable<Macro> Macros { get; set; }
    }
}