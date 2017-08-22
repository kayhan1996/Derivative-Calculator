using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public partial class Node : IEnumerable<Node> {
        private String payload;
        private Attributes attribute;
        private Node leftChild;
        private Node rightChild;
        private Node parent;

        public Node(String payload = "", Attributes type = Attributes.Empty, Node leftChild = null, Node rightChild = null) {
            this.Payload = payload;
            this.Attribute = type;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
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
                if (value != null) { value.Parent = this; };
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
                if (value != null) { value.Parent = this; }
            }
        }

        /// <summary>
        /// Accessors for the Parent node of the current object
        /// </summary>
        public Node Parent {
            get {
                return parent;
            }

            protected set {
                this.parent = value;
            }
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
            if(this.IsRoot()) {
                this.Payload = "0";
                this.leftChild = null;
                this.rightChild = null;
            } else {
                if (this.IsLeftChild()) {
                    this.Parent.leftChild = null;
                } else {
                    this.Parent.rightChild = null;
                }
            }
        }

        public override string ToString() {
            string s = "";

            if (Attribute == Attributes.Parenthesized) {
                s = "(";
            }

            if (HasLeftChild()) {
                s += leftChild.ToString();
            }
            s += Payload;
            if (HasRightChild()) {
                s += rightChild;
            }

            if (Attribute == Attributes.Parenthesized) {
                s += ")";
            }
            return s;
        }

        public IEnumerator<Node> GetEnumerator() {
            if (HasLeftChild()) {
                foreach (var v in leftChild) {
                    yield return v;
                }
            }

            yield return this;

            if (HasRightChild()) {
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
