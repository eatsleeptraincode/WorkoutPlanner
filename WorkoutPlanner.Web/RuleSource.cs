using System;
using System.Collections.Generic;
using System.Linq;
using FubuValidation;
using StructureMap;

namespace WorkoutPlanner.Web
{
    public class RuleSource : IValidationSource
    {
        readonly IContainer container;

        public RuleSource(IContainer container)
        {
            this.container = container;
        }

        public IEnumerable<IValidationRule> RulesFor(Type type)
        {
            var templateType = typeof(ModelRule<>);
            var closedType = templateType.MakeGenericType(type);
            var validationRules = container.GetAllInstances(closedType).Cast<IValidationRule>();
            return validationRules;
        }
    }
}