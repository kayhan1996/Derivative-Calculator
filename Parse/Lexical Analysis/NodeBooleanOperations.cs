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
        public bool HasLeftChild() {
            if (this.leftChild != null) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true if the current node has a right child
        /// </summary>
        /// <returns></returns>
        public bool HasRightChild() {
            if (this.rightChild != null) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the current node has a Parent
        /// </summary>
        /// <returns></returns>
        public bool HasParent() {
            if (this.Parent != null) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the current Node is the left child of the Parent,
        /// returns false if the object is not the left child or has no Parent
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsLeftChild() {
            if (this.HasParent() && this.Parent.leftChild == this) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the current Node is the right child of the Parent,
        /// returns false if the object is not the right child or has no Parent
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool IsRightChild() {
            if(this.HasParent() && this.Parent.rightChild == this) {
                return true;
            }
            return false;
        }

        public bool IsRoot() {
            if(this.Parent == null) {
                return true;
            }
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
