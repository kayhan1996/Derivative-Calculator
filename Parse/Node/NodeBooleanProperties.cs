using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public partial class Node {

        /// <summary>
        /// Returns true if the current node has a left child
        /// </summary>
        /// <returns></returns>
        public bool HasLeftChild => leftChild != null;
        /// <summary>
        /// Returns true if the current node has a right child
        /// </summary>
        /// <returns></returns>
        public bool HasRightChild => rightChild != null;

        /// <summary>
        /// Returns true if the current node has a Parent
        /// </summary>
        /// <returns></returns>
        public bool HasParent => Parent != null;

        /// <summary>
        /// Returns true if the current Node is the left child of the Parent,
        /// returns false if the object is not the left child or has no Parent
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsLeftChild => HasParent && Parent.leftChild == this;

        /// <summary>
        /// Returns true if the current Node is the right child of the Parent,
        /// returns false if the object is not the right child or has no Parent
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsRightChild => HasParent && Parent.rightChild == this;

        /// <summary>
        /// Returns true if the current node is a root node
        /// </summary>
        public bool IsRoot => !HasParent;

        /// <summary>
        /// Returns true if the current node is a leaf node
        /// </summary>
        public bool IsLeaf => !HasLeftChild && !HasRightChild;

        /// <summary>
        /// Returns true if the current node is a branch
        /// </summary>
        public bool IsBranch => !IsLeaf;

        /// <summary>
        /// Returns true if the current node is a polynomial
        /// </summary>
        public bool IsPolynomial => IsBranch && Attribute == Attributes.Exponent && LeftChild.IsVariable && RightChild.IsNumber;

        /// <summary>
        /// Returns true if the current node is a variable
        /// </summary>
        public bool IsVariable => Attribute == Attributes.Variable;

        /// <summary>
        /// Returns true if the current node is a number
        /// </summary>
        public bool IsNumber => Attribute == Attributes.Number;

        /// <summary>
        /// Returns true if the current node is a function
        /// </summary>
        public bool IsFunction => IsBranch && Attribute == Attributes.Exponent && LeftChild.IsStatement && RightChild.IsNumber;

        /// <summary>
        /// Returns true if the current node is a statement
        /// </summary>
        public bool IsStatement => IsBranch && (Payload == "+" || Payload == "-");

        /// <summary>
        /// Checks if the current node is inside brackets
        /// </summary>
        public bool IsParenthesized { get; set; }

        /// <summary>
        /// If either child matches the predicate the method returns true
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool EitherChildren(Func<Node, bool> predicate) {
            if(predicate(LeftChild) || predicate(RightChild)) { return true; }
            return false;
        }

        /// <summary>
        /// If both children match the predicate the method returns true
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool BothChildren(Func<Node, bool> predicate) {
            if (predicate(LeftChild) && predicate(RightChild)) { return true; }
            return false;
        }

        /// <summary>
        /// Returns true if Nodes a and b are the same
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsEqual(Node a, Node b) {
            if (a == null && b == null) {
                return true;
            } else if (a.Payload == b.Payload) {
                return true && IsEqual(a.leftChild, b.leftChild) && IsEqual(a.rightChild, b.rightChild);
            }
            return false;
        }

        
    }
}
