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
		private Point startpoint = new Point(0, 0);
		private List<Point> traversedPoints = new List<Point>();
		Stack<Point> availablepoints = new Stack<Point>();
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
		private static double GetpointDistance(double x1, double y1, double x2, double y2)
		{
			return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
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
				Point visitedp = new Point(CurrentX, CurrentY);
				if (!visited.Contains(visitedp))
					visited.Add(new Point(CurrentX, CurrentY));
				traversedPoints.Clear();
				availablepoints.Clear();
			}

			else if(CurrentX == x && CurrentY == y + 1)
			{
				MoveAgentNorth();
				MovetoLocation(x, y);
			}

			else if (CurrentX == x && CurrentY == y - 1)
			{
				MoveAgentSouth();
				MovetoLocation(x, y);
			}
			else if (CurrentX == x-1 && CurrentY == y)
			{
				MoveAgentEast();
				MovetoLocation(x, y);
			}
			else if (CurrentX == x + 1 && CurrentY == y)
			{
				MoveAgentWest();
				MovetoLocation(x, y);
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
						visitedneighbours.Add(p);	
					}
				}
				//pick the nearest node to move back to the first one
				
				Point nextpoint = new Point(-1,-1) ;
				double minpathdistance = 10000;
				foreach(Point v in visitedneighbours)
				{
					
					double h = GetpointDistance(v.X, v.Y, startpoint.X, startpoint.Y) + GetpointDistance(v.X, v.Y, x, y);
					if (minpathdistance > h  && !traversedPoints.Contains(v))
					{
						minpathdistance = h;						
						nextpoint = v;
					}
					else if(!traversedPoints.Contains(v))
					{
						availablepoints.Push(v);
					}
				}
				if(nextpoint.Equals(new Point(-1, -1)))
				{
					//Console.ReadLine();
					Console.BackgroundColor = ConsoleColor.Blue;
					nextpoint = availablepoints.Pop();
					Console.WriteLine("Backtracking to point " + nextpoint);
				}
				Console.WriteLine("min distance = " + minpathdistance + "min point : " + nextpoint);
				//Console.WriteLine("next point is " + nextpoint);
				traversedPoints.Add(nextpoint);
				MovetoLocation(nextpoint.X, nextpoint.Y);
				MovetoLocation(x, y);
				
			}

			

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
			if (CurrentY1 + 1 < world.Size)
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
			if (CurrentX1 + 1 < world.Size)
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
			bool st = map[CurrentX, CurrentY].Stench;
			bool gl = map[CurrentX, CurrentY].Glitter;			
			kb.tell(b, st, false, CurrentX, CurrentY);
			if (gl)
			{
				startpoint = new Point(CurrentX, CurrentY);
				MovetoLocation(0, 0);
				throw new Exception("The agent collected the gold and won");
			}
			List<Point> safe = new List<Point>();
			List<Point> notsafe = new List<Point>();
			//Asking knowledgebase about safety of neighbouring cell's safety
			foreach (Point p in visited) {
				//making neighbours for each visited node
				List<Point> neighbours = Getpointneighbours(p);	
				foreach (Point n in neighbours)
				{
					//Console.WriteLine("point ( "+n.X+","+n.Y+") is "+kb.ispit(n.X, n.Y));
					if (!kb.ispit(n.X, n.Y) && !kb.iswumpus(n.X,n.Y))
					{
						if(!safe.Contains(n))
						safe.Add(n);
					}
					else
					{
						if (!notsafe.Contains(n))
							notsafe.Add(n);
					}
				}
			}
			Console.WriteLine("Safe Nodes: ");
			foreach (Point l in safe)
			{
				//Console.Write(l+ ", ");
			}
			Console.WriteLine();
			//if there are safe nodes go to safe one
			//Console.WriteLine(safe.Count);
			List<Point> newsafe = new List<Point>();
			foreach (Point s in safe)
			{
				if (!visited.Contains(s))
				{
					newsafe.Add(s);
					
				}
			}
			//if there are safe new nodes to visit go to that node
			if (newsafe.Count !=0 )
			{
				Point nextpoint = newsafe.First();
				Console.WriteLine("Moving to location " + nextpoint);
				startpoint = new Point(CurrentX, CurrentY);
				MovetoLocation(nextpoint.X, nextpoint.Y);
				

			}
			//if there are no new safe nodes to visit pick a random new node to go to
			else
			{
				Console.WriteLine("No more safe nodes");
				bool nomoremoves = true;
				foreach(Point m in notsafe)
				{
					if (!visited.Contains(m))
					{
						Console.WriteLine("Moving to random node "+ m);
						startpoint = new Point(CurrentX, CurrentY);
						MovetoLocation(m.X, m.Y);
						nomoremoves = false;
						break;
					}
				}
				if (nomoremoves)
				{
					Console.WriteLine("Can't make any more moves");
				}
			}
			//if the agent is on a Wumpus or Pit he dies
			
			if(world.map[CurrentX,CurrentY].Pit || world.map[CurrentX, CurrentY].Wumpus)
			{
				throw new Exception("The agent is dead");
			}
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
