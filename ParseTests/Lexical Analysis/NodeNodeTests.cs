﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse.Tests {
    [TestClass()]
    public class NodeNodeTests {
        [TestMethod()]
        public void TestNodeConstructor() {
            Node a = new Node(payload: "2", type: Attributes.Number);
            Node b = new Node(payload: "3", type: Attributes.Number);
            Node n = new Node(payload: "*", type: Attributes.Term, leftChild: a, rightChild: b);

            if (n.IsRoot != true) {
                throw new Exception("Node is not a root");
            }

            if (a.IsRoot) {
                throw new Exception("Node is root");
            }

            if (b.Parent != n) {
                throw new Exception("Parent not correctly set");
            }

            if (n.Attribute != Attributes.Term) {
                throw new Exception("Attribute not set");
            }

        }

        [TestMethod()]
        public void TestReplace() {
            Node x = new Node();
            
            Node a = new Node("2", Attributes.Number);
            Node b = new Node("3", Attributes.Number);
            Node y = new Node("*", Attributes.Term, a, b);


            x.Replace(y);

            if(x.LeftChild != a) {
                throw new Exception("Left child not replaced");
            }

            if(x.RightChild != b) {
                throw new Exception("Right child not replaced");
            }

            if(x.Payload != y.Payload) {
                throw new Exception("Payload not replaced");
            }

            if(b.Parent != x) {
                throw new Exception("Child's parent not replaced");
            }
        }

        [TestMethod()]
        public void TestDelete() {
            Node x = new Node("", Attributes.Empty);
            Node y = new Node("", Attributes.Empty);
            x.LeftChild = y;

            x.LeftChild.Delete();

            if (x.HasLeftChild) {
                throw new Exception("Child not deleted");
            }
        }

        [TestMethod()]
        public void TestHasMethods() {
            var x = new Node("*", Attributes.Term);
            var a = new Node("2", Attributes.Number);
            var b = new Node("3", Attributes.Number);
            x.LeftChild = a; x.RightChild = b;

            if(x.HasParent || !a.HasParent || !b.HasParent) {
                throw new Exception("HasParent method broken");
            }

            if(!x.HasLeftChild || !x.HasRightChild || a.HasRightChild || a.HasLeftChild) {
                throw new Exception("Has*Child methods broken");
            }


        }

        [TestMethod()]
        public void TestIsMethods() {
            Node a = new Node();
            Node b = new Node();
            Node c = new Node();
            a.LeftChild = b;
            a.RightChild = c;

            if (a.IsLeaf) {
                throw new Exception("IsLeaf property reports the node is a leaf");
            }

            if (!b.IsLeaf) {
                throw new Exception("IsLeaf property reports the node is not a leaf");
            }

            if (!a.IsRoot) {
                throw new Exception("IsRoot property reports the node is not a root");
            }

            if (b.IsRoot) {
                throw new Exception("IsRoot property reports the node is a root");
            }

            if(a.IsLeftChild || a.IsRightChild) {
                throw new Exception("Is*Child property reports that the node is a child");
            }

            if (!b.IsLeftChild) {
                throw new Exception("IsLeftChild property reports the child is not a left child");
            }

            if(b.IsRightChild) {
                throw new Exception("IsRightChild property reports the child is a right child");
            }

            if (!c.IsRightChild) {
                throw new Exception("IsRightChild property reports the child is not a right child");
            }

            if (c.IsLeftChild) {
                throw new Exception("IsLeftChild property reports the child is a right child");
            }
        }
    }
}