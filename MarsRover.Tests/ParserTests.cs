using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using MarsRover.Invoker;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    [TestFixture]
    public class ParserTests
    {
        
        [Test]
        public void Is_Parser_GetOrderTypes_Works()
        {
            var strSurfaceSizing = "5 5";
            var strRoverDeploy = "1 2 N";
            var strRoverMove = "LMLMLMLMM";
            var parser = new Parser.Parser(null,null,null);
            var surfaceSizingType = parser.GetOrderType(strSurfaceSizing);
            var surfaceRoverDeploy = parser.GetOrderType(strRoverDeploy);
            var surfaceRoverMove = parser.GetOrderType(strRoverMove);
            Assert.AreEqual(OrderType.SurfaceSizing, surfaceSizingType);
            Assert.AreEqual(OrderType.RoverDeploy, surfaceRoverDeploy);
            Assert.AreEqual(OrderType.RoverMove, surfaceRoverMove);
        }

        [Test]
        public void Is_Parser_RoverDeployOrder_Works()
        {
            var roverDeploy = new Mock<IRoverDeploy>();
            var rovers = new List<IRover>();
            var rover = new Mock<IRover>();
            var surface = new Mock<ISurface>();
            var runner = new Runner( new System.Func<IRover>(() => rover.Object));

            roverDeploy.Setup(z => z.GetOrderType()).Returns(OrderType.RoverDeploy);
            runner.SetSurface(surface.Object);
            runner.SetRovers(rovers);
            runner.PlaceOrders(new List<IOrder> { roverDeploy.Object });
            runner.RunOrders();

            roverDeploy.Verify(z => z.Setter(rover.Object, surface.Object), Times.Once);
        }
    }
}
