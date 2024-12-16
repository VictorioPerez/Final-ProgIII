using System.Globalization;

namespace FinalProg.Middleware.Exceptions
{
    public class ExceptionBadRequestClient : Exception
    {
        public ExceptionBadRequestClient() : base() { }

        public ExceptionBadRequestClient(string message) : base(message) { }

        public ExceptionBadRequestClient(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
