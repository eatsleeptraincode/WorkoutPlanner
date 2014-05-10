using FubuMVC.Authentication;
using FubuMVC.Core.Continuations;
using FubuMVC.PersistedMembership;
using FubuPersistence;

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
}