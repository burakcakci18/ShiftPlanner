using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Constants
{
    public static class ErrorCodes
    {
        /// <summary>
        /// 2000-2999
        /// </summary>
        public static class GeneralService
        {
            public const int PREFIX = 2000;

            public const int VAL_INVALID_AIRPORT_CODE_EMPTY = 2001;
            public const int VAL_INVALID_AIRPORT_CODE_FORMAT = 2002;
            public const int VAL_INVALID_FREE_TEXT_LENGTH = 2003;
            public const int VAL_INVALID_AIRPORT_ICAO_CODE_EMPTY = 2004;
            public const int VAL_INVALID_AIRPORT_ICAO_CODE_FORMAT = 2005;
        }
    }
}
