using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public partial class Node : IEnumerable<Node> {
        protected String payload;
        protected Attributes attribute;
        private Node leftChild;
        private Node rightChild;
        private Node parent;
        private bool isParenthesized;

        public Node(String payload = "", Attributes attribute = Attributes.Empty, Node leftChild = null, Node rightChild = null) {
            this.Payload = payload;
            this.Attribute = attribute;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            this.IsParenthesized = false;
        }

        /// <summary>
        /// Accessors for the left child node of the current object
        /// </summary>
        public Node LeftChild {
            get {
                return leftChild;
            }

            set {
                this.leftChild = value;
                if (HasLeftChild) { value.Parent = this; };
            }
        }

        /// <summary>
        /// Accessors for the right child node of the current object
        /// </summary>
        public Node RightChild {
            get {
                return rightChild;
            }
            set {
                this.rightChild = value;
                if (HasRightChild) { value.Parent = this; }
            }
        }

        /// <summary>
        /// Accessors for the Parent node of the current object
        /// </summary>
        public Node Parent {
            get { return parent; }
            protected set { parent = value; }
        }

        /// <summary>
        /// Accessors for the type of the current object
        /// </summary>
        public Attributes Attribute {
            get { return attribute;  }
            set { attribute = value; }
        }

        public String Payload {
            get { return payload; }
            set { payload = value; }
        }

        /// <summary>
        /// Replaces the current node with Node n
        /// </summary>
        /// <param name="n"></param>
        public void Replace(Node n) {
            this.Payload = n.Payload;
            this.LeftChild = n.LeftChild;
            this.RightChild = n.RightChild;
            this.Attribute = n.Attribute;
        }

        /// <summary>
        /// Removes the current Node subtree from the parent
        /// </summary>
        public void Delete() {
            if(this.IsRoot) {
                this.Payload = "0";
                this.leftChild = null;
                this.rightChild = null;
            } else {
                if (this.IsLeftChild) {
                    this.Parent.leftChild = null;
                } else {
                    this.Parent.rightChild = null;
                }
            }
        }

        /// <summary>
        /// Finds the closest Nodes matching the predicate near n
        /// </summary>
        /// <param name="n"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static IEnumerable<Node> _BreadthFirstSearch(Node n, Func<Node, bool> predicate) {
            if (predicate(n)) {
                yield return n;
            } else {
                foreach(var child in _BreadthFirstSearch(n.LeftChild, predicate)){
                    yield return child;
                }
                foreach (var child in _BreadthFirstSearch(n.RightChild, predicate)) {
                    yield return child;
                }
            }
        }

        public IEnumerable<Node> BFS(Func<Node, bool> predicate) {
            return _BreadthFirstSearch(this, predicate);
        }

        public string ToFormattedString() {
            string s = "";

            if (IsParenthesized) {
                s = "(";
            }

            if (this.HasLeftChild) {
                s += leftChild.ToFormattedString();
            }
            if (HasRightChild && (RightChild.IsVariable || RightChild.IsPolynomial)) {

            } else {
                s += Payload;
            }
            if (this.HasRightChild) {
                s += rightChild.ToFormattedString();
            }

            if (IsParenthesized) {
                s += ")";
            }
            return s;
        }

        public override string ToString() {
            string s = "";

            if (IsParenthesized) {
                s = "(";
            }

            if (this.HasLeftChild) {
                s += leftChild;
            }
            s += Payload;
            if (this.HasRightChild) {
                s += rightChild;
            }

            if (IsParenthesized) {
                s += ")";
            }
            return s;
        }

        public IEnumerator<Node> GetEnumerator() {
            if (this.HasLeftChild) {
                foreach (var v in leftChild) {
                    yield return v;
                }
            }

            yield return this;

            if (this.HasRightChild) {
                foreach (var v in rightChild) {
                    yield return v;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            yield return this.GetEnumerator();
        }
    }
}
