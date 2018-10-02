using System;
using System.Globalization;

namespace NM.SharedKernel.Core.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException() : base() { }
        public AggregateNotFoundException(string message) : base(message) { }
        public AggregateNotFoundException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
