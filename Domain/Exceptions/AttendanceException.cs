namespace DISLAMS_Assignment.Domain.Exceptions
{
    public class AttendanceException : Exception
    {
        public AttendanceExceptionType Type { get; private set; }

        public AttendanceException(AttendanceExceptionType type, string message)
            : base(message)
        {
            Type = type;
        }

        public AttendanceException(AttendanceExceptionType type, string message, Exception inner)
            : base(message, inner)
        {
            Type = type;
        }
    }
}
