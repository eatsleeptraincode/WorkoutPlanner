using FubuCore.Reflection;
using FubuLocalization;
using FubuMVC.Authentication;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.PersistedMembership;
using FubuMVC.Validation;
using FubuPersistence;
using FubuValidation;
using FubuValidation.Fields;

namespace WorkoutPlanner.Web.Accounts
{
    public class ChangePasswordEndpoint
    {
        private readonly IPasswordHash _hash;
        private readonly ITransaction _transaction;
        private readonly IPrincipalContext _context;

        public ChangePasswordEndpoint(IPasswordHash hash, ITransaction transaction, IPrincipalContext context)
        {
            _hash = hash;
            _transaction = transaction;
            _context = context;
        }

        public ChangePasswordViewModel get_changepassword(ChangePasswordViewModel request)
        {
            return request;
        }

        public FubuContinuation post_changepassword(ChangePasswordViewModel request)
        {
            var userName = _context.Current.Identity.Name;
            _transaction.WithRepository(r =>
            {
                var user = r.FindWhere<User>(u => u.UserName == userName);
                var hash = _hash.CreateHash(request.NewPassword);
                user.Password = hash;
                r.Update(user);
            });

            return FubuContinuation.RedirectTo(new HomeModel());
        }
    }

    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordValidation : ClassValidationRules<ChangePasswordViewModel>
    {
        public ChangePasswordValidation()
        {
            Property(m => m.NewPassword).Matches(m => m.ConfirmPassword).ReportErrorsOn(m => m.ConfirmPassword);
            Property(m => m.OldPassword).Rule<OldPasswordMatches>();

        }
    }

    public class OldPasswordMatches : IFieldValidationRule
    {
        public OldPasswordMatches()
        {
            Token = WorkoutPlannerValidationKeys.PasswordsDontMatch;
        }

        public void Validate(Accessor accessor, ValidationContext context)
        {
            var oldPassword = context.GetFieldValue<string>(accessor);
            var hash = context.Service<IPasswordHash>().CreateHash(oldPassword);
            var username = context.Service<IPrincipalContext>().Current.Identity.Name;
            var user = context.Service<IEntityRepository>().FindWhere<User>(u => u.UserName == username);
            if (user.Password != hash)
            {
                context.Notification.RegisterMessage(accessor, Token);
            }
        }

        public StringToken Token { get; set; }
        public ValidationMode Mode { get; set; }
    }

    public class WorkoutPlannerValidationKeys : StringToken
    {
        public static readonly ValidationKeys PasswordsDontMatch = new ValidationKeys("Your old password does not match the existing one.");


        public WorkoutPlannerValidationKeys(string text)
            : base(null, text, namespaceByType: true)
        {   
        }
    }
}