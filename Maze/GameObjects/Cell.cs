using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.GameObjects
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool HasTopWall { get; set; } = true;
        public bool HasRightWall { get; set; } = true;
        public bool HasBottomWall { get; set; } = true;
        public bool HasLeftWall { get; set; } = true;
        public bool IsVisited { get; set; } = false;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
