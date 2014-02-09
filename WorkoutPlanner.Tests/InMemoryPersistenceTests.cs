using FubuPersistence.InMemory;
using NUnit.Framework;
using StructureMap;

namespace WorkoutPlanner.Tests
{
    public class InMemoryPersistenceTests
    {
        protected Container Container;

        [SetUp]
        public void SetUpContainer()
        {
            Container = new Container(c => c.AddRegistry<InMemoryPersistenceRegistry>());
        }
    }
}