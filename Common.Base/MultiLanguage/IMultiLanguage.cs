using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.MultiLanguage
{
    public interface IMultiLanguage
    {
        string GetMessage(int code, bool isSelectLang);
    }
}
