using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse.Tests {
    [TestClass()]
    public class EvaluateTests {
        [TestMethod()]
        public void evaluationTest() {
            Parser p = new Parser("(1+0)^2+2^2*(3+1)");
            Node n = p.parse();
            double t = Evaluate.eval(n);

            if (t == 17.0) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void multiplicationDydx() {
            Parser p = new Parser("2*x");
            Node n = p.parse();
            Node dydx = Derivator.dydx(n);

            Node realdydx = (new Parser("(0*x)+(1*2)")).parse();

            if(!Node.isEqual(dydx, realdydx)) {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void constantDydx() {
            Parser p = new Parser("10");
            Node n = p.parse();
            n = Derivator.dydx(n);

            if(n.payload != "0") {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void polynomialDydx() {
            Parser p = new Parser("4*x^3");
            Node n = Derivator.dydx(p.parse());
            Node real = new Parser("12*x^2").parse();
            if(!Node.isEqual(n, real)) {
                Assert.Fail();
            }
        }
    }
}