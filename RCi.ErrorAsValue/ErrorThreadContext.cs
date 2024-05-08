using System.Threading;

namespace RCi.ErrorAsValue
{
    public sealed record ErrorThreadContext(int ManagedThreadId, ApartmentState ApartmentState, string Name)
    {
        public static ErrorThreadContext GetCurrent()
        {
            var currentThread = Thread.CurrentThread;
            return new(currentThread.ManagedThreadId, currentThread.GetApartmentState(), currentThread.Name);
        }
        public override string ToString() =>
            $"{nameof(ManagedThreadId)}={ManagedThreadId}, {nameof(ApartmentState)}={ApartmentState}, {nameof(Name)}={Name}";
    }
}
