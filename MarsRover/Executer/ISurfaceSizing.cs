using MarsRover.Entities.Surface;

namespace MarsRover.Executer
{
    public interface ISurfaceSizing : IOrder
    {
        Dimension Dimension { get; set; }
        void Setter(ISurface Surface);
    }
}
