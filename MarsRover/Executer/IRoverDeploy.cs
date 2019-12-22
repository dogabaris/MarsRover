using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;

namespace MarsRover.Executer
{
    public interface IRoverDeploy : IOrder
    {
        Dot Dot { get; set; }
        Direction Direction { get; set; }
        void Setter(IRover Rover, ISurface Surface);
    }
}
