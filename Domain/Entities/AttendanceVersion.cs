using System.Text.Json;
using DISLAMS_Assignment.Domain.Enums;

namespace DISLAMS_Assignment.Domain.Entities
{
    public class AttendanceVersion
    {
        // EF Core needs this
        protected AttendanceVersion() { }

        public Guid Id { get; private set; }                 // IMMUTABLE
        public Guid AttendanceRecordId { get; private set; } // IMMUTABLE
        public int VersionNumber { get; private set; }       // IMMUTABLE

        // Persisted form (EF-friendly)
        public string StudentAttendanceJson { get; private set; }

        // Domain-friendly projection (NOT mapped)
        public IReadOnlyDictionary<string, bool> StudentAttendance =>
            JsonSerializer.Deserialize<Dictionary<string, bool>>(StudentAttendanceJson);

        public string CreatedBy { get; private set; }        // ACTOR
        public RoleType Role { get; private set; }            // ROLE
        public DateTime CreatedAt { get; private set; }       // AUDIT CONTEXT
        public string Reason { get; private set; }            // JUSTIFICATION

        // Domain constructor (used by application code)
        public AttendanceVersion(
            Guid recordId,
            int version,
            Dictionary<string, bool> data,
            string actor,
            RoleType role,
            string reason)
        {
            Id = Guid.NewGuid();
            AttendanceRecordId = recordId;
            VersionNumber = version;
            StudentAttendanceJson = JsonSerializer.Serialize(data);
            CreatedBy = actor;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            Reason = reason;
        }
    }
}