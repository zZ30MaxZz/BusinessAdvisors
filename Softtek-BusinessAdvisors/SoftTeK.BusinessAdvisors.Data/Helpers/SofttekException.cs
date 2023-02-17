using System.Globalization;

namespace SoftTeK.BusinessAdvisors.Data.Helpers
{
    public class SofttekException : Exception
    {
        public SofttekException() : base() { }

        public SofttekException(string message) : base(message) { }

        public SofttekException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
