using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core.UI.Elements;
using HtmlTags;

namespace WorkoutPlanner.Web.Html
{
    public class EnumBuilder<T> : IElementBuilder
    {
        private readonly T _defaultValue;

        public EnumBuilder(T defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public HtmlTag Build(ElementRequest request)
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
            var tag = new SelectTag(
                t =>
                {
                    values.Each(v => t.Option(v.ToString().SplitCamelCase(), v));
                    t.SelectByValue((request.RawValue ?? _defaultValue).ToString());
                });
            return tag;
        }
    }
}