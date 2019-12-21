using MarsRover.Entities.Surface;
using System.Collections.Generic;

namespace MarsRover.Entities.Rover
{
    public interface IRover
    {
        Dot Dot { get; set; }
        Direction Direction { get; set; }
        bool IsDeployed { get; set; }
        void LocationUpdate(Move move);
        bool Deploy(Direction _direction, ISurface _surface, Dot _dot);
        void Move(IList<Move> moves);
    }
}
