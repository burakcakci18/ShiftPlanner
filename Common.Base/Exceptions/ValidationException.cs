using Common.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Exceptions
{
    public class ValidationException : System.Exception
    {
        public CodeValuePair<int, string> MainReason { get; private set; }

        private List<CodeValuePair<int, string>> reasons = new List<CodeValuePair<int, string>>();

        public IEnumerable<CodeValuePair<int, string>> Reasons => reasons;

        public ValidationException(CodeValuePair<int, string> mainReason) : base(mainReason.Value)
        {
            MainReason = mainReason;
            reasons.Add(mainReason);
        }

        public ValidationException(int mainCode, string mainValue) : this(new CodeValuePair<int, string>(mainCode, mainValue)) { }

        public ValidationException Reason(CodeValuePair<int, string> reason)
        {
            reasons.Add(reason);
            return this;
        }

        public ValidationException Reason(int code, string reason)
        {
            return Reason(new CodeValuePair<int, string>(code, reason));
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
