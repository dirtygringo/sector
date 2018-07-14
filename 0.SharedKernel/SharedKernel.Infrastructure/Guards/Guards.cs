using System;

namespace NM.SharedKernel.Infrastructure.Guards
{
    public class Guards
    {
        public static void GuidCannotBeNullOrEmpty(Guid arg, string parameterName = null)
        {
            if (arg == null || arg == default) throw new ArgumentException(parameterName != null ? $"Parameter {parameterName} cannot be null or have default value." : "Parameter cannot be null or have default value.");
        }

        public static void GuidCannotBeEmpty(Guid? arg, string parameterName = null)
        {
            if (arg == Guid.Empty) throw new ArgumentException(parameterName != null ? $"Parameter {parameterName} cannot have default value." : "Parameter cannot have default value.");
        }

        public static void StringCannotBeNullWhiteSpaceOrEmpty(string field, string parameterName = null)
        {
            if (string.IsNullOrWhiteSpace(field) || field == string.Empty) throw new ArgumentException(parameterName != null ? $"Parameter {parameterName} cannot be null, have whitespace or be empty." : "Parameter cannot be null, have whitespace or be empty.");
        }

        public static void ComparedValuesCannotDiffer(int compareFrom, int compareTo, string message = null)
        {
            if (compareFrom != compareTo) throw new ArgumentException(message ?? "Compared values are not the same.");
        }

        public static void CannotContinuePerformingOperation(bool condition, string message)
        {
            if (condition) throw new InvalidOperationException(message);
        }
    }
}
