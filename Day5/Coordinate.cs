
namespace Aoc2021.Day5
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate &&
                   X == coordinate.X &&
                   Y == coordinate.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
