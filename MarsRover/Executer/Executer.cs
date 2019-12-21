using MarsRover.Entities.Rover;
using MarsRover.Entities.Surface;
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

        public Executer(ISurface _surface, IParser _parser)
        {
            surface = _surface;
            parser = _parser;
            rovers = new List<IRover>();
            parser.SetSurface(surface);
            parser.SetRovers(rovers);
        }

        public void Execute(string inputs)
        {
            GetInputs(inputs);
            var orders = parser.ParseOrders(inputs);
            parser.PlaceOrders(orders);
            parser.RunOrders();
            GetOutputs();
        }

        private void GetInputs(string inputs)
        {
            parser.GetInputs(inputs);
        }

        private void GetOutputs()
        {
            parser.GetOutputs();
        }
    }
}
