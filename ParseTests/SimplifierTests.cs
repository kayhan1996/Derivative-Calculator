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

    }
}
