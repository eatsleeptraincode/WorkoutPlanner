using System;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuPersistence;

namespace WorkoutPlanner.Web.Templates
{
    public class EditMacroEndpoint
    {
        private readonly IEntityRepository _repository;

        public EditMacroEndpoint(IEntityRepository repository)
        {
            _repository = repository;
        }

        public EditMacroViewModel get_macro_edit_MacroId(EditMacroViewModel request)
        {
            var macro = _repository.Find<Macro>(request.MacroId);
            var templateIds = macro.WorkoutTemplateIds.ToList();
            templateIds.Add(new Guid());
            request.CycleName = macro.Type;
            request.WorkoutTemplateIds = templateIds.Select(t => new Identifier{Id = t}).ToArray();
            return request;
        }

        public FubuContinuation post_macro_edit(EditMacroViewModel request)
        {
            var macro = _repository.Find<Macro>(request.MacroId);
            macro.WorkoutTemplateIds =
                request.WorkoutTemplateIds.Where(t => t.Id != Guid.Empty).Select(t => t.Id).ToList();
            _repository.Update(macro);
            return FubuContinuation.RedirectTo(new EditMacroViewModel {MacroId = request.MacroId});
        }
    }

    public class EditMacroViewModel
    {
        public Guid MacroId { get; set; }
        public string CycleName { get; set; }
        public Identifier[] WorkoutTemplateIds { get; set; }
    }

    public class Identifier
    {
        public Guid Id { get; set; }
    }
}