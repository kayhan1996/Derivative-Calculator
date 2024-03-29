﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestEvaluation() {
            Parser p = new Parser("(1+0)^2+2^2*(3+1)");
            Node n = p.Parse();
            double t = Evaluate.evaluate(n);

            if (t == 17.0) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void TestMultiplicationDydx() {
            Parser p = new Parser("2x");
            Node n = p.Parse();
            Node dydx = Derivator.dydx(n);

            Node realdydx = (new Parser("(0*x)+(1*2)")).Parse();

            if(!Node.IsEqual(dydx, realdydx)) {
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void TestConstantDydx() {
            Parser p = new Parser("10");
            Node n = p.Parse();
            n = Derivator.dydx(n);

            if(n.Payload != "0") {
                Assert.Fail();
            }
        }
    }
}