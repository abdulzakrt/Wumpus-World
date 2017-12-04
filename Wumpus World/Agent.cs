using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
enum Direction {East,South,North,West};
namespace Wumpus_World
{
    class Agent
    {
		private List<Point> visited = new List<Point>();
        private int score = 0;
        private Direction direction = Direction.West;
		private World world;
		private MapSquare[,] map;
        private int CurrentX = 0;
        private int CurrentY = 0;
		private Random r = new Random();
		private KnowledgeBase kb;
		public Agent (World w)
		{
			world = w;
			map = w.map;
			map[0, 0].PutAgent(this);
			visited.Add(new Point(0, 0));
			kb = new KnowledgeBase();
		}
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

		private void MovetoLocation(int x, int y)
		{
			//while (CurrentX != x || CurrentY != y)
			//{
			//	if (CurrentX < x && visited.Contains(new Point(CurrentX+1,CurrentY)))
			//	{
			//		MoveAgentEast();
			//	}
			//	else if (CurrentX > x && visited.Contains(new Point(CurrentX - 1, CurrentY)))
			//	{
			//		MoveAgentWest();
			//	}
			//	else if (CurrentY < y && visited.Contains(new Point(CurrentX, CurrentY-1)))
			//	{
			//		MoveAgentSouth();
			//	}
			//	else if(CurrentY> y && visited.Contains(new Point(CurrentX, CurrentY+1)))
			//	{
			//		MoveAgentNorth();
			//	}
			//}
			if(CurrentX == x && CurrentY == y)
			{
				
			}

			else if(CurrentX == x && CurrentY == y + 1)
			{
				MoveAgentNorth();
				//MovetoLocation(x, y - 1);
			}

			else if (CurrentX == x && CurrentY == y - 1)
			{
				MoveAgentSouth();
				//MovetoLocation(x, y + 1);
			}
			else if (CurrentX == x-1 && CurrentY == y)
			{
				MoveAgentEast();
				//MovetoLocation(x-1, y);
			}
			else if (CurrentX == x + 1 && CurrentY == y)
			{
				MoveAgentWest();
				//MovetoLocation(x + 1, y);
			}
			//else move to a neighboring visited node
			else
			{
				List<Point> neighbours = Getpointneighbours(new Point(x,y));
				List<Point> visitedneighbours = new List<Point>();
				foreach(Point p in neighbours)
				{
					if (visited.Contains(p))
					{
						//visitedneighbours.Add(p);
						MovetoLocation(p.X, p.Y);
						break;
						
					}
				}
				//pick the nearest node to move back to the first one
				//int goalpoint
				//int minpathdistance = Point.Subtract(, p1).Length;
				//Point nearestpoint = visitedneighbours.Min();
				//Console.WriteLine(nearestpoint);
			}

			Point visitedp = new Point(CurrentX, CurrentY);
			if(!visited.Contains(visitedp))
			visited.Add(new Point(CurrentX, CurrentY));


		}
		private bool MoveAgentNorth()
		{
			if (CurrentY1 - 1 >= 0)
			{

				world.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentY1--;
				world.map[CurrentX1, CurrentY1].PutAgent(this);

				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MoveAgentSouth()
		{
			if (CurrentY1 + 1 <= 3)
			{

				world.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentY1++;
				world.map[CurrentX1, CurrentY1].PutAgent(this);
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MoveAgentEast()
		{
			if (CurrentX1 + 1 <= 3)
			{

				world.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentX1++;
				world.map[CurrentX1, CurrentY1].PutAgent(this);
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MoveAgentWest()
		{
			if (CurrentX1 - 1 >= 0)
			{

				world.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentX1--;
				world.map[CurrentX1, CurrentY1].PutAgent(this);
				return true;
			}
			else
			{
				return false;
			}
		}

		//This function makes the agent take a step in the world
		public void Step()
		{
			//Telling knowledgebase agent's percept
			bool b = map[CurrentX, CurrentY].Breeze;
			kb.tell(b, false, false, CurrentX, CurrentY);

			List<Point> safe = new List<Point>();
			//Asking knowledgebase about safety of neighbouring cell's safety
			foreach (Point p in visited) {
				//making neighbours for each visited node
				List<Point> neighbours = Getpointneighbours(p);	
				foreach (Point n in neighbours)
				{
					Console.WriteLine("point ( "+n.X+","+n.Y+") is "+kb.ispit(n.X, n.Y));
					if (!kb.ispit(n.X, n.Y))
					{
						safe.Add(n);
					}
				}
			}

			//if there are safe nodes go to safe one
			Console.WriteLine(safe.Count);
			if (safe.Count != 0)
			{
				foreach (Point s in safe) {
					if(!visited.Contains(s)) { 
						
						Console.WriteLine("Moving to location" + s.X + s.Y);
						MovetoLocation(s.X, s.Y);
						break;
					}
				}
				
			}
			//else pick random node to move to
			
		}

		private List<Point> Getpointneighbours(Point p)
		{
			List<Point> neighbours = new List<Point>();
			new List<Point>();
			if (p.X + 1 < world.Size)
				neighbours.Add(new Point(p.X + 1, p.Y));
			if (p.Y + 1 < world.Size)
				neighbours.Add(new Point(p.X, p.Y + 1));
			if (p.X - 1 >= 0)
				neighbours.Add(new Point(p.X - 1, p.Y));
			if (p.Y - 1 >= 0)
				neighbours.Add(new Point(p.X, p.Y - 1));
			return neighbours;
		}
	}
}
