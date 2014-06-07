using System;
using System.Collections.Generic;
using FubuMVC.Core.Continuations;
using FubuPersistence;

namespace WorkoutPlanner.Web.Templates
{
    public class CreateMacroEndpoint
    {
        private readonly IEntityRepository _repository;

        public CreateMacroEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public CreateMacroViewModel get_macro_create(CreateMacroViewModel request)
        {
            return request;
        }

        public FubuContinuation post_macro_create(CreateMacroViewModel request)
        {
            _repository.Update(new Macro {Type = request.Type});
            return FubuContinuation.RedirectTo(new ListTemplateViewModel());
        }
    }

    public class Macro : Entity
    {
        public string Type { get; set; }
        public IList<Guid> WorkoutTemplateIds { get; set; }

        public Macro()
        {
            WorkoutTemplateIds = new List<Guid>();
        }
    }

    public class CreateMacroViewModel
    {
        public string Type { get; set; }
    }
}