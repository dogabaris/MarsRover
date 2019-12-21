using MarsRover.Entities.Rover;

namespace MarsRover.Entities.Surface
{
    public class Surface : ISurface
    {
        public Dimension dimension { get; set; }

        public bool IsInside(Dot _dot)
        {
            var isXInside = _dot.x <= dimension.xAxis && _dot.x > 0;
            var isYInside = _dot.y <= dimension.yAxis && _dot.y > 0;

            return isXInside && isYInside ? true : false;
        }

        public void SetDimension(Dimension _dimension)
        {
            dimension = _dimension;
        }

        public Dimension GetDimension()
        {
            return dimension;
        }
    }
}
