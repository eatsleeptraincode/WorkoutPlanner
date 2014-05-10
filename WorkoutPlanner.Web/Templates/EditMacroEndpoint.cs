using System;

namespace WorkoutPlanner.Web.Templates
{
    public class EditMacroEndpoint
    {
        public EditMacroViewModel get_macro_edit(EditMacroViewModel request)
        {
            return request;
        }
         
    }

    public class EditMacroViewModel
    {
        public Guid MacroId { get; set; }
    }
}