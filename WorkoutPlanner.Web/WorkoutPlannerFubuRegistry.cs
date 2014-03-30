using Bottles;
using FubuMVC.Core;
using FubuMVC.PersistedMembership;
using FubuPersistence.RavenDb;
using WorkoutPlanner.Web.Html;

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
                s.AddService<IActivator, DefaultRepPatterns>();
                s.AddService<IDocumentStoreConfigurationAction, ConnectionStringConfigurationAction>();
            });
		}
	}
}