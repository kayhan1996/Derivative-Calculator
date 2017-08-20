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
        public Node parse() {
            return Statement();
        }
        public Node Statement() {
            Node statement = Factor();

            Token op = t.getNextToken();

            Node operationNode;

            while (op.payload == "+" || op.payload == "-") {
                if (op.payload == "+") {
                    operationNode = statement + Factor();
                } else {
                    operationNode = statement - Factor();
                }
                statement = operationNode;

                op = t.getNextToken();
            }

            t.revert();

            return statement;
        }
        public Node Factor() {
            Node factor = Exponentiate();

            Token op = t.getNextToken();

            Node operationNode;
            
            while(op.payload == "*" || op.payload == "/") {
                if (op.payload == "*") {
                    operationNode = factor * Exponentiate();
                } else {
                    operationNode = factor / Exponentiate();
                }
                factor = operationNode;

                op = t.getNextToken();
            }
            
            t.revert();
            return factor;
        }
        public Node Exponentiate() {
            Node number = Number();

            Token op = t.getNextToken();

            Node operationNode;

            while (op.payload == "^") {
                operationNode = number ^ Number();
                number = operationNode;

                if (number.leftChild.type == Types.Variable && number.rightChild.type == Types.Number)
                    number.type = Types.Polynomial;

                op = t.getNextToken();

            }

            t.revert();

            return number;
        }
        public Node Number() {
            Token token = t.getNextToken();

            if (token.type == "Number") {
                Node n = new Node(token.payload, Types.Number);
                n.type = Types.Number;
                return n;
            }else if (token.type == "Variable") {
                Node n = new Node(token.payload, Types.Variable);
                n.type = Types.Variable;
                return n;
            }else if(token.type == "LeftBracket") {
                Node statement = Statement();
                if(t.getNextToken().type != "RightBracket") {
                    throw new System.Exception("Missing right bracket.");
                }
                statement.type = Types.Parenthesized;
                return statement;
            } else {
                throw new System.Exception("Unhandled Number Error.");
            }
        }
    }

    
}
