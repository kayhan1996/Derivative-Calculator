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
        public Node Parse() {
            var expression = Statement();
            return expression;
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

            /*Fixes multiplication in the forms of 2x = 2*x or 4(x+4) = 4*(x+4)*/
            Action multiplyImplicitly = () => {
                if (op.type == "Variable" || op.type == "LeftBracket") {
                    t.revert();
                    op = new Token();
                    op.payload = "*";
                    op.type = "Operator";
                }
            };
            multiplyImplicitly();

            Node operationNode;
            
            while(op.payload == "*" || op.payload == "/" || op.type == "Variable" || op.type == "LeftBracket") {
                multiplyImplicitly();
                if (op.type == "Variable") {
                    t.revert();
                    op = new Token();
                    op.payload = "*";
                }
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
                number.Attribute = Attributes.Exponent;
                op = t.getNextToken();

            }

            t.revert();

            return number;
        }
        public Node Number() {
            Token token = t.getNextToken();

            if (token.type == "Number") {
                Node number = new Node(token.payload, Attributes.Number);
                return number;
            }else if (token.type == "Variable") {
                Node n = new Node(token.payload, Attributes.Variable);
                return n;
            }else if(token.type == "LeftBracket") {
                Node statement = Statement();
                if(t.getNextToken().type != "RightBracket") {
                    throw new System.Exception("Missing right bracket.");
                }
                statement.IsParenthesized = true;
                return statement;
            } else {
                throw new System.Exception("Unhandled Factor Error.");
            }
        }
    }

    
}
