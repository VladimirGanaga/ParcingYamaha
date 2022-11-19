using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha
{
    internal class Card
    {
        public object Title { get;  set; }
        public object Price { get;  set; }

        internal void Parse(string response)
        {
            Console.WriteLine(response);
        }
    }
}
