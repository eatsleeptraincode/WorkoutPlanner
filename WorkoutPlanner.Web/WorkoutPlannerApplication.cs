using FubuMVC.Core;
using FubuMVC.StructureMap;

namespace WorkoutPlanner.Web
{
	public class WorkoutPlannerApplication : IApplicationSource
	{
	    public FubuApplication BuildApplication()
	    {
            return FubuApplication
                .For<WorkoutPlannerFubuRegistry>()
				.StructureMap<WorkoutPlannerRegistry>();
	    }
	}
}