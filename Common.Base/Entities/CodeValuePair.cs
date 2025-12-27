using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Entities
{
    public class CodeValuePair<CODE, VALUE>
    {
        public CODE Code { get; set; }
        public VALUE Value { get; set; }

        public CodeValuePair(CODE code, VALUE value)
        {
            Code = code;
            Value = value;
        }
    }
}
