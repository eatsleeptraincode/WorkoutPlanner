using System;
using System.Web;
using FubuMVC.Core;
using Raven.Client;
using Raven.Client.Indexes;
using StructureMap;

namespace WorkoutPlanner.Web
{
    public class Global : HttpApplication
    {
        private FubuRuntime _runtime;

        protected void Application_Start(object sender, EventArgs e)
        {
            _runtime = FubuApplication.BootstrapApplication<WorkoutPlannerApplication>();
            IndexCreation.CreateIndexes(typeof(ExerciseByName).Assembly,ObjectFactory.GetInstance<IDocumentStore>());
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _runtime.Dispose();
        }
    }
}