using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Parser {
        Tokenizer t;
        public Parser(string equation) {
            t = new Tokenizer(equation);
        }
        public void parse() {
            Statement();
        }
        public Node Statement() {
            Node factor1 = Factor();

            Token op = t.getNextToken();

            Node operationNode;

            while (op.payload == "+" || op.payload == "-") {
                operationNode = new Node(op.payload);
                operationNode.addLeftChild(factor1);
                operationNode.addRightChild(Factor());
                factor1 = operationNode;
                op = t.getNextToken();
            }

            t.revert();

            return factor1;
        }
        public Node Factor() {
            Node number1 = Exponentiate();

            Token op = t.getNextToken();

            Node operationNode;
            
            while(op.payload == "*" || op.payload == "/") {
                operationNode = new Node(op.payload);
                operationNode.addLeftChild(number1);
                operationNode.addRightChild(Exponentiate());
                number1 = operationNode;
                op = t.getNextToken();
            }

            t.revert();

            return number1;
        }
        public Node Exponentiate() {
            Node number1 = Number();

            Token op = t.getNextToken();

            Node operationNode;

            while (op.payload == "^") {
                operationNode = new Node(op.payload);
                operationNode.addLeftChild(number1);
                operationNode.addRightChild(Number());
                number1 = operationNode;
                op = t.getNextToken();
            }

            t.revert();

            return number1;
        }
        public Node Number() {
            Token token = t.getNextToken();

            if (token.type == "Number") {
                Node n = new Node(token.payload);
                n.type = "Number";
                return n;
            }else if (token.type == "Variable") {
                Node n = new Node(token.payload);
                n.type = "Variable";
                return n;
            }else if(token.type == "LeftBracket") {
                Node statement = Statement();
                if(t.getNextToken().type != "RightBracket") {
                    throw new System.Exception("Missing right bracket.");
                }
                statement.type = "inBrackets";
                return statement;
            } else {
                throw new System.Exception("Unhandled Number Error.");
            }
        }
    }

    public class Node {
        public String payload;
        public String type;
        public Node leftChild;
        public Node rightChild;
        public Node(String payload) {
            this.payload = payload;
            this.leftChild = null;
            this.rightChild = null;
        }
        public void addLeftChild(Node n) {
            this.leftChild = n;
        }
        public void addRightChild(Node n) {
            this.rightChild = n;
        }
        public bool hasLeftChild() {
            if(this.leftChild != null) {
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
        public string getTree(Node n) {
            string s = "";

            if (n.type == "inBrackets") {
                s = "(";
            }

            if (n.hasLeftChild()) {
                s += getTree(n.leftChild);
            }
            s += n.payload;
            if (n.hasRightChild()) {
                s += getTree(n.rightChild);
            }

            if (n.type == "inBrackets") {
                s += ")";
            }
            return s;
        }
        public override string ToString() {
            return getTree(this);
        }
    }
}
