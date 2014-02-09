using System.Collections.Generic;
using Bottles;
using Bottles.Diagnostics;
using FubuMVC.Authentication;
using FubuMVC.PersistedMembership;
using FubuPersistence;

namespace WorkoutPlanner.Web
{
    public class DefaultUser : IActivator
    {
        private readonly IPasswordHash _hash;
        private readonly ITransaction _transaction;

        public DefaultUser(IPasswordHash hash, ITransaction transaction)
        {
            _hash = hash;
            _transaction = transaction;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _transaction.WithRepository(r =>
            {
                var user = r.FindWhere<User>(u => u.UserName == "admin");
                if (user == null)
                {
                    user = new User {UserName = "admin", Password = _hash.CreateHash("password")};
                    r.Update(user);
                }
            });
        }
    }
}