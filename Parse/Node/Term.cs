using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Term {
        private double number = 1;
        private Dictionary<String, double> polynomials = new Dictionary<string, double>();
        private List<Node> functions = new List<Node>();
        public Term(Node term) {
            SortTerm(term);
        }

        private void SortTerm(Node n) {
            var factors = FindFactors(n).ToList();

            foreach(var factor in factors) {
                if(factor.Attribute == Attributes.Number) {
                    number *= double.Parse(factor.Payload);
                }else if(factor.Attribute == Attributes.Variable) {
                    var key = factor.Payload;
                    if (polynomials.ContainsKey(key)) {
                        polynomials[key] += 1;
                    } else {
                        polynomials.Add(key, 1);
                    }
                }else if(factor.Attribute == Attributes.Polynomial) {
                    var key = factor.LeftChild.Payload;
                    var value = double.Parse(factor.RightChild.Payload);

                    if (polynomials.ContainsKey(key)) {
                        polynomials[key] += value;
                    } else {
                        polynomials.Add(key, value);
                    }
                } else {
                    functions.Add(factor.Copy());
                }
            }

        }

        /// <summary>
        /// Finds the factors of a term
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private static IEnumerable<Node> FindFactors(Node term) {
            if (term.Payload != "*") {
                yield return term;
            } else {
                if (term.HasLeftChild) {
                    foreach (var subtree in FindFactors(term.LeftChild)) {
                        yield return subtree;
                    }

                    foreach (var subtree in FindFactors(term.RightChild)) {
                        yield return subtree;
                    }
                }
            }
        }

        public Node ToNodeTree() {
            var constant = new Node(number.ToString(), Attributes.Number);
            foreach(var polynomial in polynomials) {
                if (polynomial.Value != 1) {
                    var p = (new Node(polynomial.Key, Attributes.Variable)) ^ (new Node(polynomial.Value.ToString(), Attributes.Number));
                    constant *= p;
                } else {
                    var p = new Node(polynomial.Key, Attributes.Variable);
                    constant *= p;
                }
            }
            foreach(var function in functions) {
                constant *= function;
            }
            return constant;
        }

        public override string ToString() {
            string s = number.ToString();
            foreach (var polynomial in polynomials) {
                if (polynomial.Value != 1) {
                    s += polynomial.Key.ToString() + "^" + polynomial.Value.ToString();
                } else {
                    s += polynomial.Key.ToString();
                }
            }
            foreach(var function in functions) {
                s += function.ToString();
            }
            return s;

        }
    }
}
