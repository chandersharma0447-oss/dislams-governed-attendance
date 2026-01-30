using DISLAMS_Assignment.Domain.Enums;

namespace DISLAMS_Assignment.Application.StateMachines
{
    public static class AttendanceStateMachine
    {
        public static bool CanTransition(
            AttendanceState from,
            AttendanceState to,
            RoleType role)
        {
            return (from, to, role) switch
            {
                (AttendanceState.Draft, AttendanceState.Submitted, RoleType.Teacher) => true,
                (AttendanceState.Submitted, AttendanceState.Approved, RoleType.Admin) => true,
                (AttendanceState.Approved, AttendanceState.Published, RoleType.Admin) => true,
                (AttendanceState.Published, AttendanceState.Locked, RoleType.Admin) => true,
                (AttendanceState.Locked, AttendanceState.ReopenRequested, RoleType.Teacher) => true,
                (AttendanceState.ReopenRequested, AttendanceState.Corrected, RoleType.Admin) => true,
                _ => false
            };
        }
    }
}
