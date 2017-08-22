using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Evaluate {
        /// <summary>
        /// Evaluates the expression and returns its value as a double
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static double evaluate(Node expression) {
            if (expression.Attribute == Attributes.Number || expression.IsLeaf) {
                return Double.Parse(expression.Payload);
            } else {
                double x = evaluate(expression.LeftChild);
                double y = evaluate(expression.RightChild);

                if(expression.Payload == "*") {
                    return x * y;
                }else if(expression.Payload == "/") {
                    return x / y;
                }else if(expression.Payload == "+") {
                    return x + y;
                }else if(expression.Payload == "-") {
                    return x - y;
                }else if(expression.Payload == "^") {
                    return Math.Pow(x, y);
                }else {
                    throw new System.Exception("Unknown Error");
                }
                
            }
        }
    }
}
