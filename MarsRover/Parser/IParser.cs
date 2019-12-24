using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using System.Collections.Generic;

namespace MarsRover.Parser
{
    public interface IParser
    {
        IList<IOrder> ParseOrders(string inputs);
    }
}
