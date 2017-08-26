using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Derivator {
        public static Node dydx(Node n) {
            Node derivative = new Node("Empty", Attributes.Empty);

            if (n.Payload == "^") {

                if (n.IsPolynomial) {
                    derivative = (n.RightChild) * (n.LeftChild ^ (n.RightChild - 1));
                }

            } else if (n.Payload == "*") {

                derivative = (dydx(n.LeftChild) * (n.RightChild)) + (dydx(n.RightChild) * (n.LeftChild));
                derivative.LeftChild.IsParenthesized = true;
                derivative.RightChild.IsParenthesized = true;

            } else if (n.Payload == "+") {
                derivative = dydx(n.LeftChild) + dydx(n.RightChild);
            } else if (n.Payload == "/") {

                derivative = ((n.RightChild * dydx(n.LeftChild)) - (n.LeftChild * dydx(n.RightChild))) / (n.RightChild ^ 2);

            } else if (n.IsVariable) {

                derivative = new Node("1", Attributes.Number);

            } else if (n.IsNumber) {
                derivative = new Node("0", Attributes.Number);
            }

            return derivative;
        }
    }
}
