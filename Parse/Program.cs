using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class Program {
        static void Main(string[] args) {
            //3.5*((5*3)+(7+4.8))
            Parser p = new Parser("x^3");
            Node n = p.Statement();
            Console.WriteLine("Test master");

            Console.Write("Press any key to finish.");
            Console.Read();
        }
    }
}
