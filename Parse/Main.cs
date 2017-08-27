using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parse {
    class Program {
        static void Main(string[] args) {
            while (true) {
                Console.Write("f(x)=");
                var expression = Console.ReadLine();
                var Pexpression = new Parser(expression).Parse();
                Simplify.SimplifyExpression(Pexpression);
                Console.WriteLine("Simplifies to: " + Pexpression);
                var derivative = Derivator.dydx(Pexpression);
                Simplify.SimplifyExpression(derivative);
                Console.WriteLine(derivative.ToFormattedString());
            }
        }
    }
}

