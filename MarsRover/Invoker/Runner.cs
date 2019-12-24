using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;

namespace MarsRover.Invoker
{
    public class Runner : IRunner
    {
        private ISurface surface;
        private IList<IRover> rovers;
        private IEnumerable<IOrder> ordersGonnaRun;
        private readonly Func<IRover> RoverFunc;
        public Dictionary<char, Direction> Directions { get; }

        public Runner(Func<IRover> _roverFunc)
        {
            RoverFunc = _roverFunc;

            Directions = new Dictionary<char, Direction>
            {
                { 'E', Direction.East },
                { 'W', Direction.West },
                { 'N', Direction.North },
                { 'S', Direction.South }
            };
        }

        public void SetSurface(ISurface _surface)
        {
            surface = _surface;
        }

        public void SetRovers(IList<IRover> someRovers)
        {
            rovers = someRovers;
        }

        public void PlaceOrders(IList<IOrder> orders)
        {
            ordersGonnaRun = orders;
        }

        public void RunOrders()
        {
            foreach (var order in ordersGonnaRun)
            {
                setInitializers(order);
                order.Run();
            }
        }

        public string GetOutputs()
        {
            var strBuilder = new StringBuilder();
            foreach (var rover in rovers)
            {
                strBuilder.AppendLine(String.Format("{0} {1} {2}", rover.Dot.x, rover.Dot.y, Directions.FirstOrDefault(x => x.Value == rover.Direction).Key));
            }

            return strBuilder.ToString();
        }

        private void setInitializers(IOrder order)
        {
            switch (order.GetOrderType())
            {
                case OrderType.SurfaceSizing:
                    InitializeSurfaceSizing(order);
                    break;
                case OrderType.RoverDeploy:
                    InitializeRoverDeploy(order);
                    break;
                case OrderType.RoverMove:
                    InitializeRoverMove(order);
                    break;
                default:
                    break;
            }
        }

        private void InitializeSurfaceSizing(IOrder order)
        {
            var landingSurfaceSizeCommand = (ISurfaceSizing)order;
            landingSurfaceSizeCommand.Setter(surface);
        }

        private void InitializeRoverDeploy(IOrder order)
        {
            var roverDeploy = (IRoverDeploy)order;
            var newRover = RoverFunc();
            rovers.Add(newRover);
            roverDeploy.Setter(newRover, surface);
        }

        private void InitializeRoverMove(IOrder order)
        {
            var roverMove = (IRoverMove)order;
            var latestRover = rovers[rovers.Count - 1];
            roverMove.Setter(latestRover);
        }
    }
}
