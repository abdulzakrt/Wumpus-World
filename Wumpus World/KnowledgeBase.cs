using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SbsSW.SwiPlCs;
using System.IO;
using SbsSW.SwiPlCs.Exceptions;

namespace Wumpus_World
{
    class KnowledgeBase
    {
        public KnowledgeBase()
		{
			
			// Build a prolog source file (skip this step if you already have one :-)
			//string filename = Path.GetFileName("C:\\Users\\abrhm\\Source\\Repos\\Wumpus-World\\Wumpus World\\Packages\\Agentkb");
			// build the parameterstring to Initialize PlEngine with the generated file
			String[] param = { "-q", "-f", "Agentkb.pl" };
			if (!PlEngine.IsInitialized)
			{
				try
				{
					//initializing prolog engine
					PlEngine.Initialize(param);
					//PlQuery.PlCall("sensebreeze(true,[1,1])");
					//Boolean q = PlQuery.PlCall("ispit([2,1])");
					//Console.WriteLine(q);
					//PlQuery.PlCall("sensebreeze(false,[1,1])");
					// q = PlQuery.PlCall("ispit([2,1])");
					//Console.WriteLine(q);
					
				}
				catch (PlException e)
				{
					Console.WriteLine(e.MessagePl);
					Console.WriteLine(e.Message);
				}
			}
			
			//if (!PlEngine.IsInitialized)
			//{
			//	String[] param = { "-q" };  // suppressing informational and banner messages
			//	PlEngine.Initialize(param);
			//	PlQuery.PlCall("assert(wumpus(1,1))");
			//	PlQuery.PlCall("assert(wumpus(1,2))");
			//	PlQuery.PlCall("assert(father(uwe, melanie))");
			//	PlQuery.PlCall("assert(father(uwe, ayala))");

			//	var w = new PlQuery("wumpus(X,Y)");
			//	foreach(PlQueryVariables z in w.SolutionVariables)
			//	{
			//		Console.WriteLine(z["X"].ToString());

			//	}


			//	using (var q = new PlQuery("father(P, C), atomic_list_concat([P,' is_father_of ',C], L)"))
			//	{
			//		foreach (PlQueryVariables v in q.SolutionVariables)
			//			Console.WriteLine(v["L"].ToString());

			//		Console.WriteLine("all children from uwe:");
			//		q.Variables["P"].Unify("uwe");
			//		foreach (PlQueryVariables v in q.SolutionVariables)
			//			Console.WriteLine(v["C"].ToString());
			//	}
			//	PlEngine.PlCleanup();
			//	Console.WriteLine("finshed!");
			//}
		}

		

		public bool ispit(int x, int y)
        {
			String line = "ispit(["+x+","+y+"])" ;
			//Console.WriteLine(line);
			return PlQuery.PlCall(line);
		}
        public void tell(bool breeze,bool stench, bool glitter,int x,int y) {
			
			String line= "sensebreeze(a" + breeze + ",["+x+","+y+"])";
			//Console.WriteLine(line);
			PlQuery.PlCall(line);
			


		}

    }

    
}
