using Common.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Validation
{
    public class ValidationException : System.Exception
    {
        private readonly List<CodeValuePair<int, string>> _reasons = new();

        public CodeValuePair<int, string> MainReason { get; }
        public IEnumerable<CodeValuePair<int, string>> Reasons => _reasons.AsReadOnly();

        public ValidationException(CodeValuePair<int, string> mainReason)
            : base(mainReason?.Value)
        {
            MainReason = mainReason ?? throw new ArgumentNullException(nameof(mainReason));
            _reasons.Add(MainReason);
        }

        public ValidationException(int mainCode, string mainValue)
            : this(new CodeValuePair<int, string>(mainCode, mainValue))
        {
        }

        public ValidationException AddReason(CodeValuePair<int, string> reason)
        {
            if (reason != null)
                _reasons.Add(reason);
            return this;
        }

        public ValidationException AddReason(int code, string reason)
        {
            return AddReason(new CodeValuePair<int, string>(code, reason));
        }

        public override string ToString()
        {
            var message = $"ValidationException: {MainReason.Code} - {MainReason.Value}";
            if (_reasons.Count > 1)
            {
                message += "\nAdditional Reasons:";
                foreach (var reason in _reasons)
                {
                    if (!Equals(reason, MainReason))
                        message += $"\n - {reason.Code}: {reason.Value}";
                }
            }
            return message;
        }
    }
}
