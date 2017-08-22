using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Derivator {
        public static Node dydx(Node n) {
            Node derivative = new Node("0", Attributes.Number);

            if (n.Payload == "^") {

                if (n.Attribute == Attributes.Polynomial) {
                    derivative = (n.RightChild) * (n.LeftChild ^ (n.RightChild - 1));
                }

            } else if (n.Payload == "*") {

                derivative = (dydx(n.LeftChild) * (n.RightChild)) + (dydx(n.RightChild) * (n.LeftChild));
                derivative.LeftChild.Attribute = Attributes.Parenthesized;
                derivative.RightChild.Attribute = Attributes.Parenthesized;

            } else if (n.Payload == "+") {

                derivative = dydx(n.LeftChild) + dydx(n.RightChild);
                derivative.Attribute = Attributes.Parenthesized;

            } else if (n.Payload == "/") {

                derivative = ((n.RightChild * dydx(n.LeftChild)) - (n.LeftChild * dydx(n.RightChild))) / (n.RightChild ^ 2);

            } else if (n.Attribute == Attributes.Variable) {

                derivative = new Node("1", Attributes.Number);

            } else if (n.Attribute == Attributes.Number) {
                //Do nothing as the node is already set to "0"
            }

            return derivative;
        }
    }
}
