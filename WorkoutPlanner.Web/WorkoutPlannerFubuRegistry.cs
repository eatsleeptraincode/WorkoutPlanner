using Bottles;
using FubuMVC.Core;
using FubuMVC.PersistedMembership;
using FubuPersistence.RavenDb;

namespace WorkoutPlanner.Web
{
	public class WorkoutPlannerFubuRegistry : FubuRegistry
	{
		public WorkoutPlannerFubuRegistry()
		{
            Import<PersistedMembership<User>>();
            Import<ConfigureHtmlConventions>();
            Services(s =>
            {
                s.AddService<IActivator, DefaultUser>();
                s.AddService<IDocumentStoreConfigurationAction, ConnectionStringConfigurationAction>();
            });
		}
	}
}