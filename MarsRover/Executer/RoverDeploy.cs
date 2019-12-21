using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;

namespace MarsRover.Executer
{
    public class RoverDeploy : IRoverDeploy
    {
        public Dot Dot { get; set; }
        public Direction Direction { get; set; }
        public IRover Rover;
        public ISurface Surface { get; set; }

        public RoverDeploy(Dot _dot, Direction _direction, IRover _rover, ISurface _surface)
        {
            Dot = _dot;
            Direction = _direction;
            Rover = _rover;
            Surface = _surface;
        }

        public void Run()
        {
            Rover.Deploy(Direction, Surface, Dot);
        }

        public void Setter(IRover _rover, ISurface _surface)
        {
            Rover = _rover;
            Surface = _surface;
        }

        public OrderType GetOrderType()
        {
            return OrderType.RoverDeploy;
        }
    }
}
