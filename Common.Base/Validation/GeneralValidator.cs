using Common.Base.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Base.Validation
{
    public static class GeneralValidator
    {

        /// <summary>
        /// Serbest metin maksimum 250 karakter olabilir.
        /// </summary>
        public static void ValidateFreeText(string text)
        {
            if (text?.Length > 250)
                throw new ValidationException(ErrorCodes.GeneralService.VAL_INVALID_FREE_TEXT_LENGTH, "Text must be 250 characters or less.");
        }


    }
}
