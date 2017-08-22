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
            if (n.Attribute == Attributes.Number || n.IsLeaf) {
                return Double.Parse(n.Payload);
            } else {
                double x = eval(n.LeftChild);
                double y = eval(n.RightChild);

                if(n.Payload == "*") {
                    return x * y;
                }else if(n.Payload == "/") {
                    return x / y;
                }else if(n.Payload == "+") {
                    return x + y;
                }else if(n.Payload == "-") {
                    return x - y;
                }else if(n.Payload == "^") {
                    return Math.Pow(x, y);
                }else {
                    throw new System.Exception("Unkown Error");
                }
                
            }
        }
    }
}
