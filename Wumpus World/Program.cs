using System;
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
            w.PrintMap();
            w.MoveAgent();
            w.PrintMap();
        }
    }
}
