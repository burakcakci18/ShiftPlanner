using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.MultiLanguage
{
    public class MultiLanguage : IMultiLanguage
    {
        public string GetMessage(int code, bool isSelectLang)
        {
            if (!isSelectLang)
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InstalledUICulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InstalledUICulture;
            }

            string modifiedPrefix = "Code_" + code;
            var culture = CultureInfo.CurrentUICulture;
            var data = Resources.ErrorResource.ResourceManager.GetString(modifiedPrefix, culture);

            if (string.IsNullOrEmpty(data))
                data = null;
            return data;
        }
    }
}
