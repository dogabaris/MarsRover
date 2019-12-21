using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MarsRover.Parser
{
    public class Parser : IParser
    {
        private ISurface surface;
        private IList<IRover> rovers;
        private IList<IOrder> ordersGonnaRun;
        private IDictionary<Regex, OrderType> OrderTypes;
        private readonly Func<IRover> RoverFunc;
        private readonly Func<Dimension, ISurfaceSizing> SurfaceSizingJob;
        private readonly Func<IList<Move>, IRoverMove> RoverMoveJob;
        private readonly Func<Dot, Direction, IRoverDeploy> RoverDeployJob;
        private readonly IDictionary<OrderType, Func<string, IOrder>> Orders;
        private readonly IDictionary<char, Direction> Directions;
        private readonly IDictionary<char, Move> Moves;
        private readonly IDictionary<OrderType, Func<IOrder>> InitializersDict;

        public Parser(Func<Dimension, ISurfaceSizing> _SurfaceSizingJob, Func<IList<Move>, IRoverMove> _RoverMoveJob,
            Func<Dot, Direction, IRoverDeploy> _RoverDeployJob, Func<IRover> _roverFunc)
        {
            RoverFunc = _roverFunc;
            SurfaceSizingJob = _SurfaceSizingJob;
            RoverDeployJob = _RoverDeployJob;
            RoverMoveJob = _RoverMoveJob;

            Moves = new Dictionary<char, Move>
            {
                { 'M', Move.Forward },
                { 'L', Move.Left },
                { 'R', Move.Right }
            };

            Directions = new Dictionary<char, Direction>
            {
                { 'E', Direction.East },
                { 'W', Direction.West },
                { 'N', Direction.North },
                { 'S', Direction.South }
            };

            OrderTypes = new Dictionary<Regex, OrderType>
            {
                { new Regex(@"^\d+ \d+$"), OrderType.SurfaceSizing },
                { new Regex(@"^\d+ \d+ [NSEW]$"), OrderType.RoverDeploy },
                { new Regex(@"^[LRM]+$"), OrderType.RoverMove }
            };

            Orders = new Dictionary<OrderType, Func<string, IOrder>>
            {
                { OrderType.SurfaceSizing, SurfaceSizing },
                { OrderType.RoverDeploy, RoverDeploy },
                { OrderType.RoverMove, RoverMove }
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

        public IList<IOrder> ParseOrders(string inputs)
        {
            var orders = inputs.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var resOrders = new List<IOrder>();
            foreach (var order in orders)
            {
                var orderFunc = Orders[GetOrderType(order)];
                resOrders.Add(orderFunc.Invoke(order));
            }
            return resOrders;
        }

        public void PlaceOrders(IList<IOrder> orders)
        {
            ordersGonnaRun = orders;
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

        public void RunOrders()
        {
            foreach (var order in ordersGonnaRun)
            {
                setInitializers(order);
                order.Run();
            }
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

        public string GetOutputs()
        {
            var strBuilder = new StringBuilder();
            foreach (var rover in rovers)
            {
                strBuilder.AppendLine(String.Format("{0} {1} {2}", rover.Dot.x, rover.Dot.y, Directions.FirstOrDefault(x => x.Value == rover.Direction).Key));
            }

            return strBuilder.ToString();
        }

        public OrderType GetOrderType(string order)
        {
            var res = new OrderType();
            foreach (var type in OrderTypes)
            {
                if (type.Key.IsMatch(order))
                    res = type.Value;
            }

            return res;
        }

        public IOrder SurfaceSizing(string data)
        {
            var args = data.Split(' ');
            var width = int.Parse(args[0]);
            var height = int.Parse(args[1]);
            var dimension = new Dimension(width, height);

            return SurfaceSizingJob(dimension);
        }

        public IOrder RoverDeploy(string data)
        {
            var args = data.Split(' ');
            var x = int.Parse(args[0]);
            var y = int.Parse(args[1]);

            var direction = args[2][0];
            var dot = new Dot(x, y);
            var dir = Directions[direction];

            return RoverDeployJob(dot, dir);
        }

        public IOrder RoverMove(string data)
        {
            var args = data.ToCharArray();
            var moves = args.Select(x => Moves[x]).ToList();

            return RoverMoveJob(moves);
        }
    }
}
