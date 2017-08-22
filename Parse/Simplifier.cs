using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Simplify {
        public static void SimplifyExpression(Node expression) {
            ZeroMultiplication(expression);
            ZeroAddition(expression);
        }

        public static void ZeroMultiplication(Node n) {
            Func<Node, bool> predicate = x => {
                return
                x.Payload == "*" &&
                x.HasLeftChild && x.HasRightChild &&
                (x.LeftChild.Payload == "0" || x.RightChild.Payload == "0");
            };

            Action<Node> action = x => {
                x.Replace(new Node("0", Attributes.Number));
            };

            FindAndReplace(n, predicate, action);
        }

        public static void ZeroAddition(Node n) {
            Func<Node, bool> predicate = x => {
                return
                x.Payload == "+" &&
                x.HasLeftChild && x.HasRightChild &&
                (x.LeftChild.Payload == "0" || x.RightChild.Payload == "0");
            };

            Action<Node> action = x => {
                if(x.LeftChild.Payload == "0") {
                    x.Replace(x.RightChild);
                } else {
                    x.Replace(x.LeftChild);
                }
            };

            FindAndReplace(n, predicate, action);
        }

        public static void PowersOfOne(Node n) {
            Func<Node, bool> predicate = x => {
                return
                x.Payload == "^" &&
                x.RightChild.Payload == "1";
            };

            Action<Node> action = x => {
                x.Replace(x.LeftChild);
            };

            FindAndReplace(n, predicate, action);
        }

        public static void PowersOfZero(Node n) {
            Func<Node, bool> predicate = x => {
                return
                x.Payload == "^" &&
                x.RightChild.Payload == "0";
            };

            Action<Node> action = x => {
                x.Replace(new Node("1", Attributes.Number));
            };

            FindAndReplace(n, predicate, action);
        }

        private static void FindAndReplace(Node n, Func<Node, bool> p, Action<Node> a) {
            var node = n.FirstOrDefault(p);

            if (node != null) {
                a(node);
                FindAndReplace(n, p, a);
            }
        }

    }
}
