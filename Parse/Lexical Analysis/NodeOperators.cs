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
            Node n = new Node("*", Attributes.Factor);
            n.LeftChild = x1;
            n.RightChild = x2;
            return n;
        }
        public static Node operator ^(Node x1, Node x2) {
            Node n = new Node("^", Attributes.Exponent);
            n.LeftChild = x1;
            n.RightChild = x2;
            return n;
        }
        public static Node operator +(Node x1, Node x2) {
            Node n = new Node("+", Attributes.Statement);
            n.LeftChild = x1;
            n.RightChild = x2;
            return n;
        }
        public static Node operator /(Node x1, Node x2) {
            Node n = new Node("/", Attributes.Factor);
            n.LeftChild = x1;
            n.RightChild = x2;
            return n;
        }
        public static Node operator -(Node x1, Node x2) {
            Node n = new Node("-", Attributes.Statement);
            n.LeftChild = x1;
            n.RightChild = x2;
            return n;
        }
        public static Node operator ^(Node x1, double x2) {
            Node n = new Node("-", Attributes.Exponent);
            n.LeftChild = x1;
            n.RightChild = new Node(x2.ToString(), Attributes.Exponent);
            return n;
        }
        public static Node operator -(Node x1, double x2) {
            if (x1.Attribute == Attributes.Number) {
                double x = double.Parse(x1.Payload) - x2;
                Node n = new Node(x.ToString(), Attributes.Number);
                return n;
            }
            throw null;
        }
    }
}
