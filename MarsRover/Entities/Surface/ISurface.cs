using MarsRover.Entities.Rover;

namespace MarsRover.Entities.Surface
{
    public interface ISurface
    {
        Dimension dimension { get; set; }
        bool IsInside(Dot _dot);
        void SetDimension(Dimension _dimension);
        Dimension GetDimension();
    }
}
