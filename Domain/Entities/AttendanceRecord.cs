using DISLAMS_Assignment.Domain.Enums;

namespace DISLAMS_Assignment.Domain.Entities
{
    public class AttendanceRecord
    {
        public Guid Id { get; private set; }   // IMMUTABLE
        public DateOnly Date { get; private set; }   // IMMUTABLE
        public string ClassCode { get; private set; }   // IMMUTABLE

        public AttendanceState CurrentState { get; private set; }
        public Guid CurrentVersionId { get; private set; }

        public AttendanceRecord(DateOnly date, string classCode)
        {
            Id = Guid.NewGuid();
            Date = date;
            ClassCode = classCode;
            CurrentState = AttendanceState.Draft;
        }

        public void ChangeState(AttendanceState newState)
        {
            CurrentState = newState;
        }

        public void SetCurrentVersion(Guid versionId)
        {
            CurrentVersionId = versionId;
        }
    }
}
