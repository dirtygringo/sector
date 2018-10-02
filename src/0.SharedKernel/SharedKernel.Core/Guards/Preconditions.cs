using System;

namespace NM.SharedKernel.Core.Guards
{
    public class Preconditions
    {
        public static void CheckNullOrEmpty<T>(T value , string parameterName = null)
        {
            if ((typeof(T).IsValueType && value.Equals(default(T))) || value == null || value.Equals(default(T)))  throw new ArgumentException(parameterName != null ? $"Parameter {parameterName} cannot be null or have default value." : "Parameter cannot be null or have default value.");
        }

        public static void CheckNullEmptyWhitespace(string value, string parameterName = null)
        {
            if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException(parameterName != null ? $"Parameter {parameterName} cannot be null or have whitespace." : "Parameter cannot be null or have whitespace.");
            if(value.Equals(string.Empty)) throw new ArgumentException(parameterName != null ? $"Parameter {parameterName} cannot be empty." : "Parameter cannot be empty.");
        }

        public static void CheckComparison(int compareFrom, int compareTo, string message = null)
        {
            if (compareFrom != compareTo) throw new ArgumentException(message ?? "Compared values are not the same.");
        }

        public static void CheckTrue(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
        }
    }
}
