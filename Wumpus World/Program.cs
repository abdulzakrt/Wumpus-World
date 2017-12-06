﻿using System;
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
			int count = 0;
			int lived = 0;
			int died = 0;
			while (count<500){ 
				Console.Write("Wumpus World\n");
				World w = new World(10,2,1);
				w.InitializeWorld();
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
						}
						else { died++;
							
						}
						agentdead = true;
						w.PrintMap();

					Console.WriteLine(ex.Message);
				}

					//Console.ReadLine();
				}

				//stop prolog session
				PlEngine.PlCleanup();
				count++;
			}
			Console.WriteLine("Died: " + died + " Lived: " + lived);
		}
    }
}
