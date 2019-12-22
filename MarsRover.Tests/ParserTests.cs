using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Is_Parser_Constructor_Works()
        {
            var orders = new List<IOrder>();
            var parser = new Parser.Parser(null, null, null, null);
            Assert.DoesNotThrow(() => parser.PlaceOrders(orders));
            Assert.DoesNotThrow(() => parser.RunOrders());
        }

        [Test]
        public void Is_Parser_SetSurface_Works()
        {
            var surfaceSizingMock = new Mock<ISurface>();
            var parser = new Parser.Parser(null, null, null, null);
            Assert.DoesNotThrow(() => parser.SetSurface(surfaceSizingMock.Object));
        }

        [Test]
        public void Is_Parser_SetRovers_Works()
        {
            var rovers = new List<IRover>();
            var parser = new Parser.Parser(null, null, null, null);
            Assert.DoesNotThrow(() => parser.SetRovers(rovers));
        }

        [Test]
        public void Is_Parser_GetOrderTypes_Works()
        {
            var strSurfaceSizing = "5 5";
            var strRoverDeploy = "1 2 N";
            var strRoverMove = "LMLMLMLMM";
            var parser = new Parser.Parser(null, null, null, null);
            var surfaceSizingType = parser.GetOrderType(strSurfaceSizing);
            var surfaceRoverDeploy = parser.GetOrderType(strRoverDeploy);
            var surfaceRoverMove = parser.GetOrderType(strRoverMove);
            Assert.AreEqual(OrderType.SurfaceSizing, surfaceSizingType);
            Assert.AreEqual(OrderType.RoverDeploy, surfaceRoverDeploy);
            Assert.AreEqual(OrderType.RoverMove, surfaceRoverMove);
        }

        [Test]
        public void Is_Parser_SurfaceSizingOrder_Works()
        {
            var surfaceSizing = new Mock<ISurfaceSizing>();
            var surface = new Mock<ISurface>();
            var parser = new Parser.Parser(null, null, null, null);

            surfaceSizing.Setup(z => z.GetOrderType()).Returns(OrderType.SurfaceSizing);
            parser.SetSurface(surface.Object);
            parser.PlaceOrders(new List<IOrder> { surfaceSizing.Object });
            parser.RunOrders();

            surfaceSizing.Verify(z => z.Setter(surface.Object), Times.Once);
        }

        [Test]
        public void Is_Parser_RoverMoveOrder_Works()
        {
            var roverMove = new Mock<IRoverMove>();
            var rovers = new List<IRover>();
            var rover = new Mock<IRover>();
            rovers.Add(rover.Object);
            var surface = new Mock<ISurface>();
            var parser = new Parser.Parser(null, null, null, null);

            roverMove.Setup(z => z.GetOrderType()).Returns(OrderType.RoverMove);
            parser.SetSurface(surface.Object);
            parser.SetRovers(rovers);
            parser.PlaceOrders(new List<IOrder> { roverMove.Object });
            parser.RunOrders();

            roverMove.Verify(z => z.Setter(rover.Object), Times.Once);
        }

        [Test]
        public void Is_Parser_RoverDeployOrder_Works()
        {
            var roverDeploy = new Mock<IRoverDeploy>();
            var rovers = new List<IRover>();
            var rover = new Mock<IRover>();
            var surface = new Mock<ISurface>();
            var parser = new Parser.Parser(null, null, null, new System.Func<IRover>(() => rover.Object));

            roverDeploy.Setup(z => z.GetOrderType()).Returns(OrderType.RoverDeploy);
            parser.SetSurface(surface.Object);
            parser.SetRovers(rovers);
            parser.PlaceOrders(new List<IOrder> { roverDeploy.Object });
            parser.RunOrders();

            roverDeploy.Verify(z => z.Setter(rover.Object, surface.Object), Times.Once);
        }
    }
}
