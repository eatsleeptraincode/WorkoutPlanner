using FubuMVC.Core;
using FubuMVC.StructureMap;
using FubuPersistence.RavenDb;
using Serenity;
using WorkoutPlanner.Web;

namespace WorkoutPlanner.AcceptanceTests
{
    public class WorkoutPlannerSystem : FubuMvcSystem<WorkoutPlannerStoryTellerSource>
    {

    }

    public class WorkoutPlannerStoryTellerSource : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            return FubuApplication.For<WorkoutPlannerFubuRegistry>()
                .StructureMapObjectFactory(c =>
                {
                    c.AddRegistry<WorkoutPlannerRegistry>();
                    c.For<RavenDbSettings>().Use(RavenDbSettings.InMemory());
                });
        }
    }
}