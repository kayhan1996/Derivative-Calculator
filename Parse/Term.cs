using System;
using System.Collections.Generic;
using System.Linq;
using Parse;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Term {
        List<Node> Factors;
        int FactorCount;
        double Numbers;
        Dictionary<String, double> Variables;
        Dictionary<Node, double> Functions;
        public Term(Node n) {
                /*Initialize the 3 types of factors for sorting*/
                Numbers = 1;
                Variables = new Dictionary<string, double>();
                Functions = new Dictionary<Node, double>();
                /*Seperate the factors into a list for quick access*/
                Factors = n.BFS(x => x.Payload != "*").ToList();
                /*Set to 1 since a term has atleast 1 factor*/
                FactorCount = 1;
                SortFactors();
        }

        private void SortFactors() {
            foreach(var factor in Factors) {
                if(factor.Attribute == Attributes.Number) {
                    Numbers *= double.Parse(factor.Payload);
                }else if(factor.Attribute == Attributes.Variable) {
                    Update(Variables, factor.Payload, "1");
                }else if(factor.IsPolynomial) {
                    Update(Variables, factor.LeftChild.Payload, factor.RightChild.Payload);
                }else if(factor.Attribute == Attributes.Exponent) {
                    Update(Functions, factor.LeftChild, factor.RightChild.Payload);
                } else {
                    Update(Functions, factor, "1");
                }
            }
        }

        public IEnumerable<Node> ToList() {
            if(Numbers != 1.0) {
                yield return new Node(Numbers.ToString(), Attributes.Number);
            }
            foreach(var factor in Variables) {
                if(factor.Value == 1.0) {
                    yield return new Node(factor.Key, Attributes.Variable);
                } else {
                    Node n1 = new Node(factor.Key, Attributes.Factor);
                    Node n2 = new Node(factor.Value.ToString(), Attributes.Number);
                    Node tmp = n1 ^ n2;
                    yield return tmp;
                }
            }
            foreach(var factor in Functions) {
                if (factor.Value == 1.0) {
                    yield return factor.Key;
                } else {
                    Node n1 = factor.Key;
                    Node n2 = new Node(factor.Value.ToString(), Attributes.Number);
                    yield return (n1 ^ n2);
                }
            }
        }

        public Node ToNode() {
            Node tmp = new Node();
            foreach(var element in this.ToList()) {
                tmp *= element;
            }
            return tmp;
        }

        private void Update<TKey>(Dictionary<TKey, double> dict, TKey function, string exponent) {
            if (dict.ContainsKey(function)) {
                dict[function] += double.Parse(exponent);
            } else {
                dict.Add(function, double.Parse(exponent));
                FactorCount += 1;
            }
        }
    }
}
