using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    public partial class Node {
        /// <summary>
        /// Returns a list of nodes that match the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<Node> Find(Predicate<Node> predicate) {
            var n = new List<Node>();
            foreach(var descendent in this) {
                if (predicate(descendent)) {
                    n.Add(descendent);
                }
            }
            return n;
        }
    }
}
