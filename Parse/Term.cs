using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class Term {
        Factor numerator;
        Factor denomimator;

        /// <summary>
        /// Accepts a Node n that is a term
        /// </summary>
        /// <param name="n"></param>
        public Term(Node n) {
            if (n.Payload == "/") {
                numerator = new Factor(n.LeftChild);
                denomimator = new Factor(n.RightChild);
            } else if (n.Payload == "*") {
                numerator = new Factor(n);
                denomimator = null;
            } else {
                throw new Exception($"Incorrect Node n ({n.ToFormattedString()}) enters for Divider, missing payload of '/'");
            }
        }

        private void SimplifyTerm(Node n) {
            var numeratorList = numerator.ToList();
            var denominmatorList = denomimator.ToList();
        }

        public Node ToNode() {
            if (denomimator != null) {
                return numerator.ToNode() / denomimator.ToNode();
            }
            return numerator.ToNode();
        }
    }
}
