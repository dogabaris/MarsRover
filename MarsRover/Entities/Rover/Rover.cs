using MarsRover.Entities.Surface;
using System.Collections.Generic;

namespace MarsRover.Entities.Rover
{
    public class Rover : IRover
    {
        public Dot Dot { get; set; }
        public Direction Direction { get; set; }
        public bool IsDeployed { get; set; }

        public bool Deploy(Direction _direction, ISurface _surface, Dot _dot)
        {
            if (_surface.IsInside(_dot))
            {
                Direction = _direction;
                Dot = _dot;
                IsDeployed = true;
                return true;
            }
            return false;
        }

        public void LocationUpdate(Move move)
        {
            switch (move)
            {
                case Entities.Rover.Move.Forward:
                    switch (Direction)
                    {
                        case Direction.North:
                            Dot = new Dot(Dot.x, Dot.y + 1);
                            break;
                        case Direction.West:
                            Dot = new Dot(Dot.x - 1, Dot.y);
                            break;
                        case Direction.East:
                            Dot = new Dot(Dot.x + 1, Dot.y);
                            break;
                        case Direction.South:
                            Dot = new Dot(Dot.x, Dot.y - 1);
                            break;
                        default:
                            break;
                    }
                    break;
                case Entities.Rover.Move.Left:
                    switch (Direction)
                    {
                        case Direction.North:
                            Direction = Direction.West;
                            break;
                        case Direction.West:
                            Direction = Direction.South;
                            break;
                        case Direction.East:
                            Direction = Direction.North;
                            break;
                        case Direction.South:
                            Direction = Direction.East;
                            break;
                        default:
                            break;
                    }
                    break;
                case Entities.Rover.Move.Right:
                    switch (Direction)
                    {
                        case Direction.North:
                            Direction = Direction.East;
                            break;
                        case Direction.West:
                            Direction = Direction.North;
                            break;
                        case Direction.East:
                            Direction = Direction.South;
                            break;
                        case Direction.South:
                            Direction = Direction.West;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Move(IList<Move> moves)
        {
            foreach (Move move in moves)
            {
                LocationUpdate(move);
            }
        }
    }
}
