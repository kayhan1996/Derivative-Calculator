using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Evaluate {
        Node expression;
        public Evaluate(Node expression) {
            this.expression = expression;
        }

        public static double eval(Node n) {
            if (n.type == Types.Number || !(n.hasLeftChild() && n.hasRightChild())) {
                return Double.Parse(n.payload);
            } else {
                double x = eval(n.leftChild);
                double y = eval(n.rightChild);

                if(n.payload == "*") {
                    return x * y;
                }else if(n.payload == "/") {
                    return x / y;
                }else if(n.payload == "+") {
                    return x + y;
                }else if(n.payload == "-") {
                    return x - y;
                }else if(n.payload == "^") {
                    return Math.Pow(x, y);
                }else {
                    throw new System.Exception("Unkown Error");
                }
                
            }
        }
    }
}
