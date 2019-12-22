using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests
{
    [TestFixture]
    public class RoverTests
    {
        Mock<ISurface> surfaceMock;

        [SetUp]
        public void SetUp()
        {
            surfaceMock = new Mock<ISurface>();
        }

        [Test]
        public void Is_Rover_Not_Deployed_When_Created()
        {
            var rover = new Rover();
            Assert.That(!rover.IsDeployed);
        }

        [TestCase(1, 2, Direction.North)]
        [TestCase(3, 3, Direction.East)]
        public void Is_Deployed_Rover_Inside_Surface(int expectedX, int expectedY, Direction expectedDirection)
        {
            var dot = new Dot(expectedX, expectedY);
            var rover = new Rover();
            surfaceMock.Setup(z => z.IsInside(dot)).Returns(true);
            rover.Deploy(expectedDirection, surfaceMock.Object, dot);

            Assert.AreEqual(expectedDirection, rover.Direction);
            Assert.AreEqual(dot, rover.Dot);
        }

        [TestCase(1, 2, Direction.North)]
        [TestCase(3, 3, Direction.East)]
        public void Is_Rover_Deployed_True(int expectedX, int expectedY, Direction expectedDirection)
        {
            var dot = new Dot(expectedX, expectedY);
            var rover = new Rover();
            surfaceMock.Setup(z => z.IsInside(dot)).Returns(true);
            rover.Deploy(expectedDirection, surfaceMock.Object, dot);

            Assert.IsTrue(rover.IsDeployed);
        }

        [TestCase(1, 2, Direction.North)]
        [TestCase(3, 3, Direction.East)]
        public void Is_Rover_Deployed_False(int expectedX, int expectedY, Direction expectedDirection)
        {
            var dot = new Dot(expectedX, expectedY);
            var rover = new Rover();
            surfaceMock.Setup(z => z.IsInside(dot)).Returns(false);
            rover.Deploy(expectedDirection, surfaceMock.Object, dot);

            Assert.IsFalse(rover.IsDeployed);
        }

        [TestCase(1, 2, Direction.North, Move.Left, Move.Forward, Move.Left, Move.Forward,
            Move.Left, Move.Forward, Move.Left, Move.Forward, Move.Forward, 1, 3, Direction.North)]
        public void Is_Rover_Moves_As_Expected(int startX, int startY, Direction startDirection, Move order0, Move order1, Move order2, Move order3,
            Move order4, Move order5, Move order6, Move order7, Move order8, int expectedX, int expectedY, Direction expectedDirection)
        {
            var start = new Dot(startX, startY);
            var expectedDot = new Dot(expectedX, expectedY);
            var rover = new Rover();
            var orders = new List<Move> { order0, order1, order2, order3, order4, order5, order6, order7, order8 };
            surfaceMock.Setup(z => z.IsInside(start)).Returns(true);
            rover.Deploy(startDirection, surfaceMock.Object, start);
            rover.Move(orders);

            Assert.AreEqual(expectedDot, rover.Dot);
            Assert.AreEqual(expectedDirection, rover.Direction);
        }
    }
}
