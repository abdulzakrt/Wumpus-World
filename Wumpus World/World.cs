using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wumpus_World
{
    class World
    {

        public MapSquare[,] map ;
		private int size;
        Random r = new Random();
        public World() {
            map = new MapSquare[4, 4];
			size = 4;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    map[i, j] = new MapSquare();
                }
               
            }
            
        }

		public int Size { get => size; set => size = value; }

		public void InitializeWorld()
        {
            //Random Number Generator
            
            int x=0,y=0;
            //Adding a wumpus
            x = r.Next(1, 4);
            y = r.Next(1, 4);
            while (x == 0 && y == 0) {
                x = r.Next(1, 4);
                y = r.Next(1, 4);
            }
            map[x,y].Wumpus = true;
            map[x, y].Stench = true;
            if(x+1<4 && x + 1 >= 0) {
                map[x + 1, y].Stench = true;
            }
            if (x - 1 < 4 && x - 1 >= 0)
            {
                map[x - 1, y].Stench = true;
            }
            if (y + 1 < 4 &&  y + 1 >= 0)
            {
                map[x, y + 1].Stench = true;
            }
            if (y - 1 < 4 && y - 1 >= 0)
            {
                map[x, y - 1].Stench = true;
            }
            //Adding 3 pits
            for(int i = 0; i < 2; i++)
            {
                x = r.Next(0, 4);
                y = r.Next(0, 4);
                while (map[x,y].Pit || map[x, y].Wumpus || (x==0 && y==0)) {
                    x = r.Next(0, 4);
                    y = r.Next(0, 4);
                }
                map[x, y].Pit = true;
                map[x, y].Breeze = true;
                if (x + 1 < 4 && x + 1 >= 0)
                {
                    map[x + 1, y].Breeze = true;
                }
                if (x - 1 < 4 && x - 1 >= 0)
                {
                    map[x - 1, y].Breeze = true;
                }
                if (y + 1 < 4 && y + 1 >= 0)
                {
                    map[x, y + 1].Breeze = true;
                }
                if (y - 1 < 4 && y - 1 >= 0)
                {
                    map[x, y - 1].Breeze = true;
                }
            }
            //Adding Gold
            x = r.Next(0, 4);
            y = r.Next(0, 4);
            while (map[x, y].Pit || map[x, y].Wumpus || (x == 0 && y == 0))
            {
                x = r.Next(0, 4);
                y = r.Next(0, 4);
            }
            map[x, y].Glitter = true;
           
            

        }

        public void PrintMap() {
			Console.WriteLine();
			Console.WriteLine("______________________________________________");
			Console.WriteLine("   0         1         2         3");
            for (int i = 0; i < 4; i++) {
                Console.Write(i+"  ");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(map[j,i].ReturnSquare());
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("(W = Wumpus, St=Stench, Br=Breeze, Gl=Glitter, Sc=Scream, V=Visited, Sa=Safe, P=Pit, A=Agent)");
        }

        
    }
}
