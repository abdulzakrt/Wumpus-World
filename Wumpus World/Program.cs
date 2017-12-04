using System;
using SbsSW.SwiPlCs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wumpus_World
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Write("Wumpus World\n");
			World w = new World();
			w.InitializeWorld();
			Agent a = new Agent(w);
			int x;
			while (true)
			{
				w.PrintMap();
				a.Step();
				
				Console.ReadLine();
			}
			
			//stop prolog session
			PlEngine.PlCleanup();
		}
    }
}
