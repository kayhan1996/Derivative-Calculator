using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parse {
    class Program {
        static void Main(string[] args) {
            var n = new Parser("2*x^2").Parse();
            n = Derivator.dydx(n);
            Simplify.SimplifyExpression(n);
            Console.WriteLine(n);

            Console.Read();
        }
    }
}

