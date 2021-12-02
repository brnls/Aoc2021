using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2021.Day1
{
    internal class Day2
    {
        public static readonly string[] Input = File.ReadAllLines("Day2/input");

        public static long Part1()
        {
            var verticalPos = 0;
            var horizontalPos = 0;

            foreach (var input in Input)
            {
                var data = input.Split(' ');
                var val = int.Parse(data[1]);
                if (data[0] == ("forward"))
                    horizontalPos+= val;
                else if (data[0] == ("up"))
                {
                    verticalPos-= val;
                }
                else
                {
                    verticalPos+= val;
                }
            }

            return verticalPos * horizontalPos;
        }

        public static long Part2()
        {
            var verticalPos = 0;
            var horizontalPos = 0;
            var aim = 0;

            foreach (var input in Input)
            {
                var data = input.Split(' ');
                var val = int.Parse(data[1]);
                if (data[0] == ("forward"))
                {
                    horizontalPos+= val;
                    verticalPos += (aim * val);
                }
                else if (data[0] == ("up"))
                {
                    aim -= val;
                }
                else
                {
                    aim += val;
                }
            }

            return verticalPos * horizontalPos;
        }
    }
}
