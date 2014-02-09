using StructureMap.Configuration.DSL;

namespace WorkoutPlanner.Web
{
	public class WorkoutPlannerRegistry : Registry
	{
		public WorkoutPlannerRegistry()
		{
            Scan(x => {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
		}
	}
}