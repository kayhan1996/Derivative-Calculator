using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Derivator {
        public static Node dydx(Node n) {
            Node derivative = new Node("0", Types.Number);
            if(n.payload == "^") {
                if(n.leftChild.type == Types.Variable && n.rightChild.type == Types.Number) {
                    derivative = (n.rightChild) * (n.leftChild ^ (n.rightChild - 1));
                }
            }else if(n.payload == "*") {
                derivative = (dydx(n.leftChild) * (n.rightChild)) + (dydx(n.rightChild) * (n.leftChild));
                derivative.leftChild.type = Types.Parenthesized;
                derivative.rightChild.type = Types.Parenthesized;
            }else if(n.payload == "+") {
                derivative = dydx(n.leftChild) + dydx(n.rightChild);
                derivative.type = Types.Parenthesized;
            } else if(n.payload == "/") {
                derivative = ((n.rightChild * dydx(n.leftChild)) - (n.leftChild * dydx(n.rightChild))) / (n.rightChild ^ 2);
            }else if(n.type == Types.Variable) {
                derivative = new Node("1", Types.Number);
            }else if(n.type == Types.Number) {
                //Do nothing as the node is already set to "0"
            }
            return derivative;
            
        }
    }
}
