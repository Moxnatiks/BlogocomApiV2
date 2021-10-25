using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlogocomApiV2.Exceptions
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message = "Error!") : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
