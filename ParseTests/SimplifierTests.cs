using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse.Tests {
    [TestClass()]
    public class SimplifierTests {
        
        [TestMethod()]
        public void TestZeroMultiplicationSimplify() {
            var x = new Parser("0*1*3+1");
            var n = x.Parse();
            Simplify.ZeroMultiplication(n);

            if(n.ToString() != "0+1") {
                Assert.Fail();
            }

            n = new Parser("1*0*3+1").Parse();
            Simplify.ZeroMultiplication(n);

            if(n.ToString() != "0+1") {
                Assert.Fail();
            }

            n = new Parser("1*3*0+1").Parse();
            Simplify.ZeroMultiplication(n);

            if (n.ToString() != "0+1") {
                Assert.Fail();
            }

            n = new Parser("1*3+1*0").Parse();
            Simplify.ZeroMultiplication(n);

            if(n.ToString() != "1*3+0") {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void TestZeroAdditionSimplify() {
            var p = new Parser("0+1+3").Parse();
            Simplify.ZeroAddition(p);

            if(p.ToString() != "1+3") {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void TestPowersOfOne() {
            var p = new Parser("x^1").Parse();
            Simplify.PowersOfOne(p);
            if (p.ToString() != "x") {
                Assert.Fail();
            }

            p = new Parser("37^1").Parse();
            Simplify.PowersOfOne(p);
            if(p.ToString() != "37") {
                Assert.Fail();
            }

            p = new Parser("1+2*x^1+1").Parse();
            Simplify.PowersOfOne(p);
            if(p.ToString() != "1+2*x+1") {
                Assert.Fail();
            }

            p = new Parser("1+x^2").Parse();
            Simplify.PowersOfOne(p);
            if(p.ToString() != "1+x^2") {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void TestPowersOfZero() {
            var p = new Parser("x^0").Parse();
            Simplify.PowersOfZero(p);
            if(p.ToString() != "1") {
                Assert.Fail();
            }

            p = new Parser("2^0").Parse();
            Simplify.PowersOfZero(p);
            if(p.ToString() != "1") {
                Assert.Fail();
            }

            p = new Parser("1+2^0").Parse();
            Simplify.PowersOfZero(p);
            if(p.ToString() != "1+1") {
                Assert.Fail();
            }

            p = new Parser("1+x^0").Parse();
            Simplify.PowersOfZero(p);
            if (p.ToString() != "1+1") {
                Assert.Fail();
            }

            p = new Parser("1+(x+5)^0").Parse();
            Simplify.PowersOfZero(p);
            if (p.ToString() != "1+1") {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestNumericalPowers() {
            var p = new Parser("2^3x^4y+5^3x").Parse();
            Simplify.NumericalPowers(p);
            if(p.ToFormattedString() != "8x^4y+125x") {
                throw new Exception("Numerical Power error, expression simplified to: " + p.ToString());
            }
        }

        [TestMethod]
        public void TestSimplifyFactors() {
            var p = new Parser("3xxyyx").Parse();
            Simplify.Terms(p);

            if (p.ToFormattedString() != "3x^3y^2") {
                throw new Exception($"Factor Simplification failed, factor {p} simplifed to {p.ToFormattedString()}");
            }
        }

        [TestMethod]
        public void TestSimplifyExpressions() {
            string s = "(3x^2)(2x^2)";
            var p = new Parser(s).Parse();
            Simplify.SimplifyExpression(p);

            if (p.ToString() != "6*x^4") {
                throw new Exception($"Simplification failed {s} simplified to {p.ToFormattedString()}");
            }
        }
    }
}
