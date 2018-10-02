using System;
using System.Reflection;

namespace NM.SharedKernel.Core.Helpers
{
    public class ConstructorInitializer
    {
        public static T Construct<T>(Type[] paramType, object[] paramValues)
        {
            return (T)(typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, paramType, null)?.Invoke(paramValues));
        }
    }
}
