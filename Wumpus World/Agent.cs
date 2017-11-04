using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum Direction {East,South,North,West};
namespace Wumpus_World
{
    class Agent
    {
        private int score = 0;
        Direction direction = Direction.West;
        int CurrentX = 0;
        int CurrentY = 0;
        public int Score { get => score; set => score = value; }
        public Direction Direction { get => direction; set => direction = value; }
        public int CurrentX1 { get => CurrentX; set => CurrentX = value; }
        public int CurrentY1 { get => CurrentY; set => CurrentY = value; }

        

    }
}
