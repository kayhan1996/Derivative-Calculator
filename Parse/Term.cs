using System;
using System.Collections.Generic;
using System.Linq;
using Parse;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public class Term {
        List<Node> Factors;
        double Numbers;
        Dictionary<String, double> Variables;
        Dictionary<Node, double> Functions;
        public Term(Node n) {
            if(n.Payload == "*") {
                /*Initialize the 3 types of factors for sorting*/
                Numbers = 1;
                Variables = new Dictionary<string, double>();
                Functions = new Dictionary<Node, double>();
                /*Seperate the factors into a list for quick access*/
                Factors = n.BFS(x => x.Payload != "*").ToList();
            }
        }

        private void SortFactors() {
            foreach(var factor in Factors) {
                if(factor.Attribute == Attributes.Number) {
                    Numbers *= double.Parse(factor.Payload);
                }else if(factor.Attribute == Attributes.Variable) {
                    Update(Variables, factor.Payload, "1");
                }else if(factor.Attribute == Attributes.Polynomial) {
                    Update(Variables, factor.LeftChild.Payload, factor.RightChild.Payload);
                }
            }
        }

        public static void Update<TKey>(Dictionary<TKey, double> dict, TKey function, string exponent) {
            if (dict.ContainsKey(function)) {
                dict[function] += double.Parse(exponent);
            } else {
                dict.Add(function, double.Parse(exponent));
            }
        }
    }
}
