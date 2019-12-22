using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void Is_SurfaceSizing_Constructor_Works_As_Expected()
        {
            var dim = new Dimension(5, 5);
            var surfaceSizingOrder = new SurfaceSizing(dim);
            Assert.AreEqual(dim, surfaceSizingOrder.Dimension);
        }

        [Test]
        public void Is_SurfaceSizing_GetOrderType_Returns_True()
        {
            var dim = new Dimension(5, 5);
            var surfaceSizingOrder = new SurfaceSizing(dim);
            Assert.AreEqual(OrderType.SurfaceSizing, surfaceSizingOrder.GetOrderType());
        }

        [Test]
        public void Is_SurfaceSizing_Set_Dimension()
        {
            var dim = new Dimension(5, 5);
            var surfaceSizingOrder = new SurfaceSizing(dim);
            Assert.AreEqual(dim, surfaceSizingOrder.Dimension);
        }

        [Test]
        public void Is_SurfaceSizing_Run_Works_Without_Exception()
        {
            var dim = new Dimension(5, 5);
            var surfaceMock = new Mock<ISurface>();
            var surfaceSizingOrder = new SurfaceSizing(dim);
            surfaceSizingOrder.Setter(surfaceMock.Object);
            Assert.DoesNotThrow(() => surfaceSizingOrder.Run());
        }

        [Test]
        public void Is_SurfaceSizing_Setter_Works()
        {
            var dim = new Dimension(5, 5);
            var surfaceMock = new Mock<ISurface>();
            var surfaceSizingOrder = new SurfaceSizing(dim);
            Assert.DoesNotThrow(() => surfaceSizingOrder.Setter(surfaceMock.Object));
        }

        [Test]
        public void Is_RoverDeploy_GetOrderType_Returns_True()
        {
            var dot = new Dot(1, 2);
            var surfaceMock = new Mock<ISurface>();
            var roverMock = new Mock<IRover>();
            var roverDeployOrder = new RoverDeploy(dot, Direction.North, roverMock.Object, surfaceMock.Object);
            Assert.AreEqual(OrderType.RoverDeploy, roverDeployOrder.GetOrderType());
        }

        [TestCase(1, 2, Direction.North)]
        public void Is_RoverDeploy_Constructor_Works_As_Expected(int expectedX, int expectedY, Direction expectedDirection)
        {
            var dot = new Dot(expectedX, expectedY);
            var surfaceMock = new Mock<ISurface>();
            var roverMock = new Mock<IRover>();
            var roverDeployOrder = new RoverDeploy(dot, Direction.North, roverMock.Object, surfaceMock.Object);
            Assert.AreEqual(expectedDirection, roverDeployOrder.Direction);
            Assert.AreEqual(dot, roverDeployOrder.Dot);
        }

        [Test]
        public void Is_RoverDeploy_Setter_Works()
        {
            var dot = new Dot(5, 5);
            var surfaceMock = new Mock<ISurface>();
            var roverMock = new Mock<IRover>();
            var roverDeployOrder = new RoverDeploy(dot, Direction.North, roverMock.Object, surfaceMock.Object);
            Assert.DoesNotThrow(() => roverDeployOrder.Setter(roverMock.Object, surfaceMock.Object));
        }

        [Test]
        public void Is_RoverDeploy_Run_Works_Without_Exception()
        {
            var dot = new Dot(5, 5);
            var surfaceMock = new Mock<ISurface>();
            var roverMock = new Mock<IRover>();
            var roverDeployOrder = new RoverDeploy(dot, Direction.North, roverMock.Object, surfaceMock.Object);
            roverDeployOrder.Setter(roverMock.Object, surfaceMock.Object);
            Assert.DoesNotThrow(() => roverDeployOrder.Run());
        }

        [Test]
        public void Is_RoverMove_GetOrderType_Returns_True()
        {
            var roverMock = new Mock<IRover>();
            var moveList = new List<Move> { Move.Left, Move.Right, Move.Forward };
            var roverMoveOrder = new RoverMove(roverMock.Object, moveList);
            Assert.AreEqual(OrderType.RoverMove, roverMoveOrder.GetOrderType());
        }
        
        [Test]
        public void Is_RoverMove_Constructor_Works_As_Expected()
        {
            var roverMock = new Mock<IRover>();
            var moveList = new List<Move> { Move.Left, Move.Right, Move.Forward };
            var roverMoveOrder = new RoverMove(roverMock.Object, moveList);
            Assert.AreEqual(moveList, roverMoveOrder.Moves);
        }

        [Test]
        public void Is_RoverMove_Setter_Works()
        {
            var roverMock = new Mock<IRover>();
            var moveList = new List<Move> { Move.Left, Move.Right, Move.Forward };
            var roverMoveOrder = new RoverMove(roverMock.Object, moveList);
            Assert.DoesNotThrow(() => roverMoveOrder.Setter(roverMock.Object));
        }

        [Test]
        public void Is_RoverMove_Run_Works_Without_Exception()
        {
            var roverMock = new Mock<IRover>();
            var moveList = new List<Move> { Move.Left, Move.Right, Move.Forward };
            var roverMoveOrder = new RoverMove(roverMock.Object, moveList);
            roverMoveOrder.Setter(roverMock.Object);
            Assert.DoesNotThrow(() => roverMoveOrder.Run());
        }
    }
}
