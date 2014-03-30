using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FubuCore;
using FubuMVC.Core.UI;
using FubuMVC.Core.UI.Elements;
using FubuMVC.TwitterBootstrap.Forms;
using FubuPersistence;
using HtmlTags;
using WorkoutPlanner.Web.Athletes;
using WorkoutPlanner.Web.RepPatterns;
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
            Editors.IfPropertyIs<RepScheme>().BuildBy(new EnumBuilder<RepScheme>(RepScheme.StraightSets));
            Editors.IfPropertyHasAttribute<RepPatternAttribute>().BuildBy<RepPatternDropDown>();
        }
    }

    public class RepPatternDropDown : IElementBuilder
    {
        public HtmlTag Build(ElementRequest request)
        {
            var repository = request.Get<IEntityRepository>();

            var values = repository.All<RepPatternCollection>().First().Patterns;

            var tag = new SelectTag(
                t =>
                {
                    values.Each(v => t.Option( v.Pattern.Select(p => p.Amount.ToString(CultureInfo.InvariantCulture))
                .ToArray().Join(","), v.Id));
                    t.SelectByValue((request.RawValue).ToString());
                });
            return tag;
        }
    }

    public class RepPatternAttribute : Attribute
    {
    }
}