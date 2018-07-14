using System;

namespace NM.SharedKernel.Infrastructure.Helpers
{
    public static class IndexedGuid
    {
        #region Fields

        private static Func<Guid> _generator = _generatorCore;

        private static readonly Func<Guid> _generatorCore = () =>
        {
            var destinationArray = Guid.NewGuid().ToByteArray();
            var time = new DateTime(1900, 1, 1);
            var now = DateTime.Now;
            var span = new TimeSpan(now.Ticks - time.Ticks);
            var timeOfDay = now.TimeOfDay;
            var bytes = BitConverter.GetBytes(span.Days);
            var array = BitConverter.GetBytes((long) (timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(array);
            Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
            Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);
            return new Guid(destinationArray);
        };

        #endregion

        #region Properties

        public static Guid Empty => Guid.Parse("00000000-0000-0000-0000-000000000000");
        
        #endregion

        #region Methods

        public static Guid Generate() => _generator();

        public static void Reset()
        {
            _generator = _generatorCore;
        }

        public static IDisposable Stub(Guid value)
        {
            _generator = () => value;
            return new DisposableAction(Reset);
        }

        #endregion

        #region HelperClass

        private class DisposableAction : IDisposable
        {
            private readonly Action _action;

            public DisposableAction(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action();
            }
        }

        #endregion
    }
}
