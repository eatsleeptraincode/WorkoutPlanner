using Bottles;
using FubuMVC.Core;
using FubuMVC.PersistedMembership;
using FubuMVC.Validation;
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
                s.AddService<IDocumentStoreConfigurationAction, ConnectionStringConfigurationAction>();
            });

            //TODO: figure out applying validation rules

//            Import<ValidationSettings>()
//            this.Validation(v =>
//            {
//                v.Actions
//                    .Include(a => a.HasInput && a.InputType().Name.EndsWith("ViewModel"));
//                v.Failures
//                    .If(a => a.InputType() != null && a.InputType().Name.EndsWith("ViewModel"))
//                    .TransferBy<ViewModelDescriptor>();
//            });
		}
	}
}