using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using MarsRover.Invoker;
using MarsRover.Parser;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Tests
{
    [TestFixture]
    public class ExecuterTests
    {
        [Test]
        public void Is_Executer_Constructor_Works()
        {
            var mockSurface = new Mock<ISurface>();
            var mockParser = new Mock<IParser>();
            var mockRunner = new Mock<IRunner>();
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("5 5");
            strBuilder.AppendLine("1 2 N");
            strBuilder.AppendLine("LMLMLMLMM");
            strBuilder.AppendLine("3 3 E");
            strBuilder.Append("MMRMMRMRRM");
            var inputs = strBuilder.ToString();

            var executer = new Executer.Executer(mockSurface.Object, mockParser.Object, mockRunner.Object);
            Assert.DoesNotThrow(() => executer.Execute(inputs));
        }

        [Test]
        public void Is_Executer_GetOutput_Works()
        {
            var mockSurface = new Mock<ISurface>();
            var mockRunner = new Mock<IRunner>();
            var mockParser = new Mock<IParser>();
            var expectedOrders = new IOrder[] { };
            var strBuilder = new StringBuilder();

            mockParser.Setup(x => x.ParseOrders(null)).Returns(expectedOrders);

            var executer = new Executer.Executer(mockSurface.Object, mockParser.Object, mockRunner.Object);
            executer.Execute(null);

            var output = executer.GetOutputs();

            Assert.AreEqual(output, null);
            mockRunner.Verify(z => z.RunOrders(), Times.Once());
        }

        [Test]
        public void Is_Executer_Runner_SetSurface()
        {
            var mockSurface = new Mock<ISurface>();
            var mockParser = new Mock<IParser>();
            var mockRunner = new Mock<IRunner>();

            var executer = new Executer.Executer(mockSurface.Object, mockParser.Object, mockRunner.Object);

            mockRunner.Verify(z => z.SetSurface(mockSurface.Object), Times.Once());
        }

        [Test]
        public void Is_Executer_Runner_SetRovers()
        {
            var mockSurface = new Mock<ISurface>();
            var mockRovers = new List<IRover>();
            var mockParser = new Mock<IParser>();
            var mockRunner = new Mock<IRunner>();

            var executer = new Executer.Executer(mockSurface.Object, mockParser.Object, mockRunner.Object);

            mockRunner.Verify(z => z.SetRovers(mockRovers), Times.Once());
        }
    }
}
