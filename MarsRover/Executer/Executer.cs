using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
using MarsRover.Invoker;
using MarsRover.Parser;
using System;
using System.Collections.Generic;

namespace MarsRover.Executer
{
    public class Executer : IExecuter
    {
        private readonly ISurface surface;
        private readonly IList<IRover> rovers;
        private readonly IParser parser;
        private readonly ISurfaceSizing surfaceSizing;
        private readonly IRunner runner;

        public Executer(ISurface _surface, IParser _parser, IRunner _runner)
        {
            rovers = new List<IRover>();
            surface = _surface;
            parser = _parser;
            runner = _runner;
            runner.SetSurface(surface);
            runner.SetRovers(rovers);
        }

        public void Execute(string inputs)
        {
            var orders = parser.ParseOrders(inputs);
            runner.PlaceOrders(orders);
            runner.RunOrders();
            GetOutputs();
        }

        public string GetOutputs()
        {
            return runner.GetOutputs();
        }
    }
}
