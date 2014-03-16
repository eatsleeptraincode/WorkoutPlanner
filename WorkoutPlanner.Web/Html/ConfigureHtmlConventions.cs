using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.UI;
using FubuMVC.Core.UI.Elements;
using FubuMVC.TwitterBootstrap.Forms;
using HtmlTags;
using WorkoutPlanner.Web.Athletes;
using WorkoutPlanner.Web.Workouts;

namespace WorkoutPlanner.Web.Html
{
    public class ConfigureHtmlConventions : HtmlConventionRegistry
    {
        public ConfigureHtmlConventions()
        {
            FieldChrome<BootstrapFieldChrome>();

            Editors.If(a => a.Accessor.Name.EndsWith("Id")).Attr("type", "hidden");
            Editors.If(a => a.Accessor.Name.Contains("Pass")).Attr("type", "password");
            Editors.IfPropertyIs<ExerciseGroup>().BuildBy(new EnumBuilder<ExerciseGroup>(ExerciseGroup.A));
            Editors.IfPropertyIs<Gender>().BuildBy(new EnumBuilder<Gender>(Gender.Male));
        }
    }
}