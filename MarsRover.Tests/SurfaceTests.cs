using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using Moq;
using NUnit.Framework;

namespace MarsRover.Tests
{
    [TestFixture]
    public class SurfaceTests
    {
        [TestCase(5, 5)]
        public void Are_Sizes_Correct_In_Surface(int expectedX, int expectedY)
        {
            var dim = new Dimension(expectedX, expectedY);
            var surface = new Surface();
            surface.SetDimension(dim);
            var surfaceDims = surface.GetDimension();

            Assert.AreEqual(dim, surfaceDims);
        }

        [TestCase(5, 5, 1, 1)]
        public void Is_Dot_In_Surface(int surfaceX, int surfaceY, int dotX, int dotY)
        {
            var dot = new Dot(dotX, dotY);
            var dim = new Dimension(surfaceX, surfaceY);
            var surface = new Surface();
            surface.SetDimension(dim);
            var isInside = surface.IsInside(dot);
            Assert.IsTrue(isInside);
        }

        [TestCase(5, 5, 7, 8)]
        public void Is_Dot_Not_In_Surface(int surfaceX, int surfaceY, int dotX, int dotY)
        {
            var dot = new Dot(dotX, dotY);
            var dim = new Dimension(surfaceX, surfaceY);
            var surface = new Surface();
            surface.SetDimension(dim);
            var isInside = surface.IsInside(dot);
            Assert.IsFalse(isInside);
        }
    }
}
