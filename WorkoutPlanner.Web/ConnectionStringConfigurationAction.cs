using FubuPersistence.RavenDb;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace WorkoutPlanner.Web
{
    public class ConnectionStringConfigurationAction : IDocumentStoreConfigurationAction
    {
        public void Configure(IDocumentStore documentStore)
        {
            var store = documentStore as DocumentStore;
            if (store == null)
            {
                return;
            }
            store.ConnectionStringName = "RavenDB";
        }
    }
}