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

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public int CurrentX1
        {
            get
            {
                return CurrentX;
            }

            set
            {
                CurrentX = value;
            }
        }

        public int CurrentY1
        {
            get
            {
                return CurrentY;
            }

            set
            {
                CurrentY = value;
            }
        }
    }
}
