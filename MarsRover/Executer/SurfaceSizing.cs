using MarsRover.Entities.Surface;

namespace MarsRover.Executer
{
    public class SurfaceSizing : ISurfaceSizing
    {
        public Dimension Dimension { get; set; }
        private ISurface surface;

        public SurfaceSizing(Dimension _dimension)
        {
            Dimension = _dimension;
        }

        public void Run()
        {
            surface.SetDimension(Dimension);
        }
        
        public OrderType GetOrderType()
        {
            return OrderType.SurfaceSizing;
        }

        public void Setter(ISurface Surface)
        {
            surface = Surface;
        }

        public Dimension GetDimension()
        {
            return Dimension;
        }
    }
}
