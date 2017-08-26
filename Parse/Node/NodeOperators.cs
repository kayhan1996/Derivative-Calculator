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
            /*Special case for when multiplying nodes in a list, the first node is empty*/
            if(x1.Attribute == Attributes.Empty) { return x2; }

            Node n = new Node("*", Attributes.Term);
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
            /*Special case for when adding nodes in a list, the first node is empty*/
            if (x1.Attribute == Attributes.Empty) { return x2; }

            Node n = new Node("+", Attributes.Statement);
            n.LeftChild = x1;
            n.RightChild = x2;
            return n;
        }
        public static Node operator /(Node x1, Node x2) {
            Node n = new Node("/", Attributes.Term);
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
            Node n = new Node("^", Attributes.Exponent);
            n.LeftChild = x1;
            n.RightChild = new Node(x2.ToString(), Attributes.Exponent);
            return n;
        }
        public static Node operator -(Node x1, double x2) {
            if (x1.IsNumber) {
                double x = double.Parse(x1.Payload) - x2;
                Node n = new Node(x.ToString(), Attributes.Number);
                return n;
            }
            throw null;
        }
    }
}
