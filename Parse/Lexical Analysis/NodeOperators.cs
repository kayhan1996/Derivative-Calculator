using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse
{
    public partial class Node
    {
        public static Node operator *(Node x1, Node x2){
            Node n = new Node("*", Types.Factor);
            n.addLeftChild(x1);
            n.addRightChild(x2);
            return n;
        }
        public static Node operator ^(Node x1, Node x2) {
            Node n = new Node("^", Types.Exponent);
            n.addLeftChild(x1);
            n.addRightChild(x2);
            return n;
        }
        public static Node operator +(Node x1, Node x2) {
            Node n = new Node("+", Types.Statement);
            n.addLeftChild(x1);
            n.addRightChild(x2);
            return n;
        }
        public static Node operator /(Node x1, Node x2) {
            Node n = new Node("/", Types.Factor);
            n.addLeftChild(x1);
            n.addRightChild(x2);
            return n;
        }
        public static Node operator -(Node x1, Node x2) {
            Node n = new Node("-", Types.Statement);
            n.addLeftChild(x1);
            n.addRightChild(x2);
            return n;
        }
        public static Node operator ^(Node x1, double x2) {
            Node n = new Node("-", Types.Exponent);
            n.addLeftChild(x1);
            n.addRightChild(new Node(x2.ToString(), Types.Exponent));
            return n;
        }
        public static Node operator -(Node x1, double x2) {
            if (x1.type == Types.Number) {
                double x = double.Parse(x1.payload) - x2;
                Node n = new Node(x.ToString(), Types.Number);
                return n;
            }
            throw null;
        }
    }
}
