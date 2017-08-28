using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Simplify {
        public static void SimplifyExpression(Node expression) {
            NumericalPowers(expression);
            ZeroMultiplication(expression);
            ZeroAddition(expression);
            PowersOfOne(expression);
            PowersOfZero(expression);
            Terms(expression);
        }

        public static void ZeroMultiplication(Node n) {
            Func<Node, bool> predicate = x => x.Payload == "*" && x.IsBranch && x.EitherChildren(a=>a.Payload == "0");

            Action<Node> action = x => x.Replace(new Node("0", Attributes.Number));

            FindAndReplace(n, predicate, action);
        }

        public static void ZeroAddition(Node n) {
            Func<Node, bool> predicate = x => x.IsStatement && x.EitherChildren(a => a.Payload == "0");


            Action<Node> action = x => 
            {
                if(x.LeftChild.Payload == "0") {
                    x.Replace(x.RightChild);
                } else {
                    x.Replace(x.LeftChild);
                }
            };

            FindAndReplace(n, predicate, action);
        }

        public static void PowersOfOne(Node n) {
            Func<Node, bool> predicate = x =>
                x.Payload == "^" &&
                x.RightChild.Payload == "1";

            Action<Node> action = x => x.Replace(x.LeftChild);

            FindAndReplace(n, predicate, action);
        }

        public static void PowersOfZero(Node n) {
            Func<Node, bool> predicate = x =>
                x.Payload == "^" &&
                x.RightChild.Payload == "0";

            Action<Node> action = x => {
                x.Replace(new Node("1", Attributes.Number));
            };

            FindAndReplace(n, predicate, action);
        }

        public static void NumericalPowers(Node n) {
            Func<Node, bool> predicate = x => x.Payload == "^" && x.BothChildren(a => a.IsNumber);

            Action<Node> action = x => {
                var eBase = double.Parse(x.LeftChild.Payload);
                var ePower = double.Parse(x.RightChild.Payload);
                var tmp = Math.Pow(eBase, ePower);
                var num = new Node(tmp.ToString(), Attributes.Number);
                x.Replace(num);
            };

            FindAndReplace(n, predicate, action);
        }

        public static void Terms(Node n) {
            var terms = n.BFS(x => !x.IsStatement).ToList();
            var expression = new Node();
            foreach (var term in terms) {
                expression += new Factor(term).ToNode();
            }

            n.Replace(expression);
        }
 
        public static void FindAndReplace(Node n, Func<Node, bool> p, Action<Node> a) {
            var node = n.FirstOrDefault(p);

            while (node != null) {
                a(node);
                node = n.FirstOrDefault(p);
            }
        }
    }
}
