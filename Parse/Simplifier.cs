using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class Simplifier {
        private Node syntaxTree;
        public Simplifier(Node n) {
            syntaxTree = n;
        }

        public void simplify() {
            Stack<Double> Numbers = new Stack<double>();
            Dictionary<String, double> Variables = new Dictionary<String, double>();
            Stack<Node> Statements = new Stack<Node>();

            foreach(Node n in syntaxTree) {

                if (n.Attribute == Attributes.Number) {
                    Numbers.Push(Double.Parse(n.Payload));
                }else if (n.Attribute == Attributes.Variable) {
                    if (Variables.ContainsKey(n.Payload)) {
                        Variables[n.Payload] += 1.0;
                    } else {
                        Variables.Add(n.Payload, 1.0);
                    }
                }else if(n.Attribute == Attributes.Polynomial) {
                    string variable = n.LeftChild.Payload;
                    int exponent = int.Parse(n.RightChild.Payload);
                    if (Variables.ContainsKey(n.LeftChild.Payload)) {
                        Variables[variable] += exponent;
                    } else {
                        Variables.Add(variable, exponent);
                    }
                }
            }

            Node st = new Node("", Attributes.Empty);
            foreach(var key in Variables) {
                Console.WriteLine(key);
            }

        }
    }
}
