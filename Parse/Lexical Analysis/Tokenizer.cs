using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Tokenizer {
        String equation;
        int index;
        bool inRevert;
        Token previousToken;
        public Tokenizer(String equation) {
            this.equation = equation;
            index = 0;
            previousToken = new Token();
            inRevert = false;
        }

        public void revert() {
            inRevert = true;
        }
        public bool isEnd() {
            if(index >= equation.Length) {
                return true;
            }
            return false;
        }

        bool isOperator(char c) {
            char[] x = {'^', '*', '/', '+', '-'};
            if (x.Contains(c)) {
                return true;
            }
            return false;
        }

        public Token getNextToken() {
            Token returnToken = new Token("END", "END");
            if (inRevert) {
                returnToken = previousToken;
                inRevert = false;
            } else if (isEnd()) {
                //do nothing
            } else if (equation[index] >= 'a' && equation[index] <= 'z') {
                returnToken = new Token(equation[index].ToString(), "Variable");
                index += 1;
            } else if (equation[index] >= '0' && equation[index] <= '9') {
                string cache = "";
                while (!isEnd() && equation[index] >= '0' && equation[index] <= '9') {
                    cache += equation[index];
                    index += 1;
                }

                if (!isEnd() && equation[index] == '.') {
                    cache += ".";
                    index += 1;
                    while (!isEnd() && equation[index] >= '0' && equation[index] <= '9') {
                        cache += equation[index];
                        index += 1;
                    }
                }
                returnToken = new Token(cache, "Number");
            } else if (equation[index] == '(') {
                returnToken = new Token("(", "LeftBracket");
                index += 1;
            } else if (equation[index] == ')') {
                returnToken = new Token(")", "RightBracket");
                index += 1;
            } else if (isOperator(equation[index])) {
                returnToken = new Token(equation[index].ToString(), "Operator");
                index += 1;
            }
            previousToken = returnToken;
            return returnToken;
        }
    }

    public struct Token {
        public static int id;
        public string payload;
        public string type;

        public Token(string payload, string type) {
            this.payload = payload;
            this.type = type;
            id += 1;
        }

        public override string ToString() {
            return "<" + type + ":" + payload + ">";
        }
    }
}
