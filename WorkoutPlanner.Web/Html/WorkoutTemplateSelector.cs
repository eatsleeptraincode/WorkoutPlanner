using System;
using System.Collections.Generic;
using FubuMVC.Core.UI.Elements;
using FubuPersistence;
using HtmlTags;
using WorkoutPlanner.Web.Templates;

namespace WorkoutPlanner.Web.Html
{
    public class WorkoutTemplateSelector : IElementBuilder
    {
        public HtmlTag Build(ElementRequest request)
        {
            var repository = request.Get<IEntityRepository>();
            var templates = repository.All<WorkoutTemplate>();
            var tag = new SelectTag(t =>
            {
                templates.Each(temp => t.Option(string.Format("{0} {1}", temp.Type, temp.Phase), temp.Id));

                t.Option(string.Empty, Guid.Empty);
                t.SelectByValue(request.RawValue);

            });
            return tag;
        }
    }
}