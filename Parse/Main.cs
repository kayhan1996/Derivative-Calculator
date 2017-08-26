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
                var derivative = Derivator.dydx(Pexpression);
                Simplify.SimplifyExpression(derivative);
                Console.WriteLine(derivative.ToString());
            }
            /*
            var n = new Parser("2*x^2").Parse();
            n = Derivator.dydx(n);
            Simplify.SimplifyExpression(n);
            Console.WriteLine(n);

            Console.Read();
            */
        }
    }
}

