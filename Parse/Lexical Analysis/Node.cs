using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public partial class Node : IEnumerable<Node> {
        public String payload;
        public Types type;
        public Node leftChild;
        public Node rightChild;
        public Node parent;

        public Node(String payload, Types type) {
            this.payload = payload;
            this.type = type;

            this.leftChild = null;
            this.rightChild = null;
        }
        public void addLeftChild(Node n) {
            this.leftChild = n;
            n.parent = this;
        }
        public void addRightChild(Node n) {
            this.rightChild = n;
            n.parent = this;
        }
        public bool hasLeftChild() {
            if (this.leftChild != null) {
                return true;
            }
            return false;
        }
        public bool hasRightChild() {
            if (this.rightChild != null) {
                return true;
            }
            return false;
        }
        public bool hasParent() {
            if(this.parent != null) {
                return true;
            }
            return false;
        }
        public static bool isEqual(Node a, Node b) {
            if(a == null && b == null) {
                return true;
            }else if (a.payload == b.payload) {
                return true && isEqual(a.leftChild, b.leftChild) && isEqual(a.rightChild, b.rightChild);
            }
            return false;
        }
        public void replaceCurrentNode(Node n) {
            if (hasParent()) {
                if(parent.leftChild == this) {
                    parent.leftChild = n;
                } else {
                    parent.rightChild = n;
                }
            }
        }
        public string getTree(Node n) {
            string s = "";

            if (n.type == Types.Parenthesized) {
                s = "(";
            }

            if (n.hasLeftChild()) {
                s += getTree(n.leftChild);
            }
            s += n.payload;
            if (n.hasRightChild()) {
                s += getTree(n.rightChild);
            }

            if (n.type == Types.Parenthesized) {
                s += ")";
            }
            return s;
        }

        public void delete() {
            if(this.parent != null) {
                if(this.parent.leftChild == this) {
                    this.parent.leftChild = null;
                } else {
                    this.parent.rightChild = null;
                }
            } else {
                this.payload = "0";
                this.leftChild = null;
                this.rightChild = null;
            }
        }

        

        public override string ToString() {
            return getTree(this);
        }

        public IEnumerator<Node> GetEnumerator() {
            if (hasLeftChild()) {
                foreach (var v in leftChild) {
                    yield return v;
                }
            }

            yield return this;

            if (hasRightChild()) {
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
