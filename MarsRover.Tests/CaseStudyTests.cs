using Autofac;
using MarsRover.Executer;
using NUnit.Framework;
using System.Reflection;
using System.Text;

namespace MarsRover.Tests
{
    [TestFixture]
    public class CaseStudyTests
    {
        private IContainer container;

        [SetUp]
        public void SetUp()
        {
            var programAssembly = Assembly.GetAssembly(typeof(Program));

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(programAssembly)
                .AsImplementedInterfaces();

            container = builder.Build();
        }

        [TearDown]
        public void TearDown()
        {
            container.Dispose();
        }

        [Test]
        public void TestStudyCase()
        {
            var executer = container.Resolve<IExecuter>();
            
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("5 5");
            strBuilder.AppendLine("1 2 N");
            strBuilder.AppendLine("LMLMLMLMM");
            strBuilder.AppendLine("3 3 E");
            strBuilder.Append("MMRMMRMRRM");
            var inputs = strBuilder.ToString();

            strBuilder.Clear();
            strBuilder.AppendLine("1 3 N");
            strBuilder.AppendLine("5 1 E");
            var expectedOutputs = strBuilder.ToString();

            executer.Execute(inputs);
            var outputs = executer.GetOutputs();

            Assert.AreEqual(outputs, expectedOutputs);
        }
    }
}
