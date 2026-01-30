using DISLAMS_Assignment.Domain.Enums;

namespace DISLAMS_Assignment.Domain.Entities
{
    public class AuditLog
    {
        public AuditLog() { }
        public Guid Id { get; private set; }     // IMMUTABLE
        public Guid EntityId { get; private set; }   // ATTENDANCE RECORD

        public string Action { get; private set; }
        public AttendanceState FromState { get; private set; }
        public AttendanceState ToState { get; private set; }

        public string Actor { get; private set; }     //WHO
        public RoleType Role { get; private set; }    // ROLE / CONTEXT
        public string Reason { get; private set; }    //  WHY (when applicable)
        public DateTime Timestamp { get; private set; }    //  WHEN

        public AuditLog(
            Guid entityId,
            string action,
            AttendanceState from,
            AttendanceState to,
            string actor,
            RoleType role,
            string reason)
        {
            Id = Guid.NewGuid();
            EntityId = entityId;
            Action = action;
            FromState = from;
            ToState = to;
            Actor = actor;
            Role = role;
            Reason = reason;
            Timestamp = DateTime.UtcNow;
        }
    }
}
