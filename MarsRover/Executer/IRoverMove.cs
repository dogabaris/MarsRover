using MarsRover.Entities.Rover;
using System.Collections.Generic;

namespace MarsRover.Executer
{
    public interface IRoverMove : IOrder
    {
        IList<Move> Moves { get; set; }
        void Setter(IRover Rover);
    }
}
