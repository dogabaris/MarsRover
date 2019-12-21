using MarsRover.Entities.Rover;
using System.Collections.Generic;

namespace MarsRover.Executer
{
    public class RoverMove : IRoverMove
    {
        public IList<Move> Moves { get; set; }
        private IRover Rover;

        public RoverMove(IRover _rover, IList<Move> _moves)
        {
            Moves = _moves;
            Rover = _rover;
        }

        public void Run()
        {
            Rover.Move(Moves);
        }

        public void Setter(IRover _rover)
        {
            Rover = _rover;
        }

        public OrderType GetOrderType()
        {
            return OrderType.RoverMove;
        }
    }
}
