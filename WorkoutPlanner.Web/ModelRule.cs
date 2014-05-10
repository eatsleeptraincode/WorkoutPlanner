using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuValidation;

namespace WorkoutPlanner.Web
{
    public abstract class ModelRule<T> : IValidationRule where T : class
    {
        private ValidationContext ctx;
        public void Validate(ValidationContext context)
        {
            ctx = context;
            Validate();
        }

        protected abstract void Validate();

        protected string GetValue(Expression<Func<T, object>> expr)
        {
            var accessor = Accessor(expr);
            return (string)accessor.GetValue(ctx.Target);
        }

        protected void RegisterError(string message, Accessor accessor)
        {
            var token = new ValidationKeys(message);
            var msg = new NotificationMessage(token);
            ctx.Notification.RegisterMessage(accessor, msg);
        }

        protected Accessor Accessor(Expression<Func<T, object>> expr)
        {
            return ReflectionHelper.GetAccessor(expr);
        }
    }
}