using System;
using System.Collections.Generic;
using System.Linq;
using Bottles;
using Bottles.Diagnostics;
using FubuMVC.Authentication;
using FubuMVC.PersistedMembership;
using FubuPersistence;
using WorkoutPlanner.Web.RepPatterns;

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

    public class DefaultRepPatterns : IActivator
    {
        private readonly ITransaction _transaction;

        public DefaultRepPatterns(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            _transaction.WithRepository(r =>
            {
                if (!r.All<RepPatternCollection>().Any())
                {
                    var col = new RepPatternCollection
                    {
                        Patterns = new List<RepPattern>
                        {
                            new RepPattern
                            {
                                Id = Guid.NewGuid(),
                                Pattern = new[]
                                {
                                    new Rep {Amount = 5},
                                    new Rep {Amount = 3},
                                    new Rep {Amount = 1}
                                }
                            },
                            new RepPattern
                            {
                                Id = Guid.NewGuid(),
                                Pattern =
                                    new[]
                                    {
                                        new Rep {Amount = 3}, 
                                        new Rep {Amount = 3}, 
                                        new Rep {Amount = 2},
                                        new Rep {Amount = 2}, 
                                        new Rep {Amount = 1}, 
                                        new Rep {Amount = 1}
                                    }
                            }
                        }
                    };
                    r.Update(col);
                }
                
            });
        }
    }
}