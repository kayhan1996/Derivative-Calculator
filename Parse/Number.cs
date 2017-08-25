using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse {
    class Number : Node{
        public Number(string payload) : base(payload, Attributes.Number) {
        }
    }
}
