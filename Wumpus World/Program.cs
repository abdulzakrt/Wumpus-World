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
			//Console.Write("Wumpus World\n");
			//World w = new World();

			//w.InitializeWorld();
			//w.PrintMap();
			//w.MoveAgent();
			//w.PrintMap();
			
			if (!PlEngine.IsInitialized)
			{
				String[] param = { "-q" };  // suppressing informational and banner messages
				PlEngine.Initialize(param);
				PlQuery.PlCall("assert(father(martin, inka))");
				PlQuery.PlCall("assert(father(uwe, gloria))");
				PlQuery.PlCall("assert(father(uwe, melanie))");
				PlQuery.PlCall("assert(father(uwe, ayala))");
				using (var q = new PlQuery("father(P, C), atomic_list_concat([P,' is_father_of ',C], L)"))
				{
					foreach (PlQueryVariables v in q.SolutionVariables)
						Console.WriteLine(v["L"].ToString());

					Console.WriteLine("all children from uwe:");
					q.Variables["P"].Unify("uwe");
					foreach (PlQueryVariables v in q.SolutionVariables)
						Console.WriteLine(v["C"].ToString());
				}
				PlEngine.PlCleanup();
				Console.WriteLine("finshed!");
			}
		}
    }
}
