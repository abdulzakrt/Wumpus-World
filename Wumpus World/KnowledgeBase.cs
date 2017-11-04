using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wumpus_World
{
    class KnowledgeBase
    {
        LinkedList<Rule> rulelist= new LinkedList<Rule>();

        bool ask()
        {
            return false;
        }
        bool tell() {
            return false;
        }

    }

    
}
