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

            foreach(Node n in ParseSyntaxTree(syntaxTree)) {

                if (n.type == Types.Number) {
                    Numbers.Push(Double.Parse(n.payload));
                }else if (n.type == Types.Variable) {
                    if (Variables.ContainsKey(n.payload)) {
                        Variables[n.payload] += 1.0;
                    } else {
                        Variables.Add(n.payload, 1.0);
                    }
                }else if(n.type == Types.Polynomial) {
                    string variable = n.leftChild.payload;
                    int exponent = int.Parse(n.rightChild.payload);
                    if (Variables.ContainsKey(n.leftChild.payload)) {
                        Variables[variable] += exponent;
                    } else {
                        Variables.Add(variable, exponent);
                    }
                }
            }

            Node st = new Node("", Types.Empty);
            foreach(var key in Variables) {
                Console.WriteLine(key);
            }

        }

        private IEnumerable<Node> ParseSyntaxTree(Node n) {
            if (n.hasLeftChild()) {
                foreach(Node subn in ParseSyntaxTree(n.leftChild))
                    yield return subn;
            }
            yield return n;
            if (n.hasRightChild()) {
                foreach (Node subn in ParseSyntaxTree(n.rightChild))
                    yield return subn;
            }
        }
    }
}
