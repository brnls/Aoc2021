
namespace Aoc2021.Day5
{
    public class Line
    {
        public Coordinate Start { get; set; } = default!;
        public Coordinate End { get; set; } = default!;


        public static LineOrientation DetermineLineType(Line coordinates)
        {
            if (coordinates.IsVerticalLine())
                return LineOrientation.Vertical;
            else if (coordinates.IsHorizontalLine())
                return LineOrientation.Horizontal;
            else if (coordinates.IsDiagonalLineEqual())
                return LineOrientation.Diagonal_Positive;
            else if (coordinates.IsDiagonalLineOpposite())
                return LineOrientation.Diagonal_Negative;
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
                Start.X < End.X && Start.Y < End.Y;
        }
        public bool IsDiagonalLineOpposite()
        {
            return
                Start.X < End.X && Start.Y > End.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Line coordinates &&
                   EqualityComparer<Coordinate>.Default.Equals(Start, coordinates.Start) &&
                   EqualityComparer<Coordinate>.Default.Equals(End, coordinates.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }
    }
}
