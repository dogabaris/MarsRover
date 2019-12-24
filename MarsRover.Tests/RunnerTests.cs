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
    public class RunnerTests
    {
        [Test]
        public void Is_Runner_Constructor_Works()
        {
            var orders = new List<IOrder>();
            var runner = new Runner(null);
            Assert.DoesNotThrow(() => runner.PlaceOrders(orders));
            Assert.DoesNotThrow(() => runner.RunOrders());
        }

        [Test]
        public void Is_Runner_SetSurface_Works()
        {
            var surfaceSizingMock = new Mock<ISurface>();
            var runner = new Runner(null);
            Assert.DoesNotThrow(() => runner.SetSurface(surfaceSizingMock.Object));
        }

        [Test]
        public void Is_Runner_SetRovers_Works()
        {
            var rovers = new List<IRover>();
            var runner = new Runner(null);
            Assert.DoesNotThrow(() => runner.SetRovers(rovers));
        }

        [Test]
        public void Is_Runner_SurfaceSizingOrder_Works()
        {
            var surfaceSizing = new Mock<ISurfaceSizing>();
            var surface = new Mock<ISurface>();
            var parser = new Parser.Parser(null, null, null);
            var runner = new Runner(null);

            surfaceSizing.Setup(z => z.GetOrderType()).Returns(OrderType.SurfaceSizing);
            runner.SetSurface(surface.Object);
            runner.PlaceOrders(new List<IOrder> { surfaceSizing.Object });
            runner.RunOrders();

            surfaceSizing.Verify(z => z.Setter(surface.Object), Times.Once);
        }

        [Test]
        public void Is_Runner_RoverMoveOrder_Works()
        {
            var roverMove = new Mock<IRoverMove>();
            var rovers = new List<IRover>();
            var rover = new Mock<IRover>();
            rovers.Add(rover.Object);
            var surface = new Mock<ISurface>();
            var runner = new Runner(null);

            roverMove.Setup(z => z.GetOrderType()).Returns(OrderType.RoverMove);
            runner.SetSurface(surface.Object);
            runner.SetRovers(rovers);
            runner.PlaceOrders(new List<IOrder> { roverMove.Object });
            runner.RunOrders();

            roverMove.Verify(z => z.Setter(rover.Object), Times.Once);
        }
    }
}
