using System;
using SbsSW.SwiPlCs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Wumpus_World
{
    class Program
    {
		static void Main(string[] args)
		{
			int count = 0;
			int lived = 0;
			int died = 0;
			while (count<1){ 
				Console.Write("Wumpus World\n");

				//Random World
				World w = new World(4, 2, 1);
				w.InitializeRandomWorld();

				//World in Book
				//World w = new World(4,2,1);
				//List<Point> pits = new List<Point>();
				//pits.Add(new Point(2, 0));
				//pits.Add(new Point(2, 2));
				//List<Point> wumpuses = new List<Point>();
				//wumpuses.Add(new Point(0, 2));
				//Point gold = new Point(1, 2);
				//WorldPlacements places = new WorldPlacements(wumpuses,pits,gold);
				//w.InitializeSpecificWorld(places);


				//w.InitializeRandomWorld();
				Agent a = new Agent(w);
				bool agentdead = false;
				
				while (!agentdead)
				{
					w.PrintMap();
					try
					{
						a.Step();
					}
					catch (Exception ex)
					{

						
						if (ex.Message.Equals("The agent collected the gold and won"))
						{
							lived++;
							a.Performance += 1000;
						}
						else { died++;
							a.Performance -= 1000;
						}
						agentdead = true;
						w.PrintMap();

					Console.WriteLine(ex.Message);
					Console.WriteLine("Performance: "+a.Performance);
					}

					Console.ReadLine();
				}

				//stop prolog session
				PlEngine.PlCleanup();
				count++;
			}
			Console.WriteLine("Died: " + died + " Lived: " + lived);
		}
    }
}
