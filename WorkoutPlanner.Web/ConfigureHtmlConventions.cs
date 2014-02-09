using FubuMVC.Core.UI;
using FubuMVC.TwitterBootstrap.Forms;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web
{
    public class ConfigureHtmlConventions : HtmlConventionRegistry
    {
        public ConfigureHtmlConventions()
        {
            FieldChrome<BootstrapFieldChrome>();

            Editors.If(a => a.Accessor.Name.EndsWith("Id")).Attr("type", "hidden");
            Editors.If(a => a.Accessor.Name.Contains("Pass")).Attr("type", "password");
            Editors.IfPropertyIs<ExerciseGroup>().BuildBy(new EnumBuilder<ExerciseGroup>(ExerciseGroup.A));
        }
    }
}