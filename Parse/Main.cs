using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parse {
    class Program {
        static void Main(string[] args) {
            Parser p = new Parser("x*x*x*x^3*y*z");
            Node n = p.parse();
            string s = n.ToString();
        }
    }
}

