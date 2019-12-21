namespace MarsRover.Entities.Surface
{
    public struct Dimension
    {
        public int xAxis { get; set; }
        public int yAxis { get; set; }

        public Dimension(int _xAxis, int _yAxis)
        {
            xAxis = _xAxis;
            yAxis = _yAxis;
        }
    }
}
