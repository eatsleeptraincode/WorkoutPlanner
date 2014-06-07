using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FubuMVC.Core.UI.Elements;
using FubuPersistence;
using HtmlTags;
using WorkoutPlanner.Web.RepPatterns;

namespace WorkoutPlanner.Web.Html
{
    public class RepPatternDropDown : IElementBuilder
    {
        public HtmlTag Build(ElementRequest request)
        {
            var repository = request.Get<IEntityRepository>();

            var values = repository.Find<RepPatternCollection>(RepPatternCollection.StaticId).Patterns;

            var tag = new SelectTag(
                t =>
                {
                    values.Each(v => t.Option(v.Pattern.Select(p => p.Amount.ToString(CultureInfo.InvariantCulture))
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