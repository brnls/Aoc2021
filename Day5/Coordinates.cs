
namespace Aoc2021.Day5
{
    public class Coordinates
    {
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }


        public static Line DetermineLineType(Coordinates coordinates)
        {
            if (coordinates.IsVerticalLine())
                return Line.Vertical;
            else if (coordinates.IsHorizontalLine())
                return Line.Horizontal;
            else if (coordinates.IsDiagonalLineEqual())
                return Line.Diagonal_Equal;
            else if (coordinates.IsDiagonalLineOpposite())
                return Line.Diagonal_Opposite;
            else
                throw new Exception("unknown line type");
        }

        public bool IsVerticalLine()
        {
            return Start.X == End.X && Start.Y != End.Y;
        }

        public bool IsHorizontalLine()
        {
            return Start.X != End.X && Start.Y == End.Y;
        }

        public bool IsDiagonalLineEqual()
        {
            return
                (Start.X == Start.Y && End.X == End.Y);
        }
        public bool IsDiagonalLineOpposite()
        {
            return
                (Start.X == End.Y && Start.Y == End.X);
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinates coordinates &&
                   EqualityComparer<Coordinate>.Default.Equals(Start, coordinates.Start) &&
                   EqualityComparer<Coordinate>.Default.Equals(End, coordinates.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }
    }
}
