using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Executer;
using System.Collections.Generic;

namespace MarsRover.Invoker
{
    public interface IRunner
    {
        void SetSurface(ISurface _surface);
        void SetRovers(IList<IRover> someRovers);
        void PlaceOrders(IList<IOrder> orders);
        void RunOrders();
        string GetOutputs();
    }
}
