using DISLAMS_Assignment.Application.StateMachines;
using DISLAMS_Assignment.Domain.Entities;
using DISLAMS_Assignment.Domain.Enums;
using DISLAMS_Assignment.Infrastructure.Repositories;

namespace DISLAMS_Assignment.Application.Services
{
    public class AttendanceService
    {
        private readonly AttendanceRepository _repo;

        public AttendanceService(AttendanceRepository repo)
        {
            _repo = repo;
        }

        // 1️⃣ Create / Mark Attendance (Draft)
        public Guid CreateDraft(
            DateOnly date,
            string classCode,
            Dictionary<string, bool> attendance,
            string actor,
            RoleType role)
        {
            if (role != RoleType.Teacher && role != RoleType.Admin)
                throw new Exception("Only Teacher/Admin can mark attendance");

            var record = new AttendanceRecord(date, classCode);
            _repo.SaveRecord(record);

            var version = new AttendanceVersion(
                record.Id,
                version: 1,
                attendance,
                actor,
                role,
                "Initial marking");

            record.SetCurrentVersion(version.Id);
            _repo.SaveVersion(version);

            _repo.SaveAudit(new AuditLog(
                record.Id,
                "Create Draft",
                AttendanceState.Draft,
                AttendanceState.Draft,
                actor,
                role,
                "Initial attendance draft"));

            return record.Id;
        }

        // 2️⃣ Submit Attendance
        public void Submit(Guid recordId, string actor, RoleType role)
        {
            Transition(
                recordId,
                AttendanceState.Submitted,
                actor,
                role,
                "Attendance submitted");
        }

        // 3️⃣ Approve Attendance
        public void Approve(Guid recordId, string actor, RoleType role)
        {
            Transition(
                recordId,
                AttendanceState.Approved,
                actor,
                role,
                "Attendance approved");
        }

        // 4️⃣ Publish Attendance
        public void Publish(Guid recordId, string actor, RoleType role)
        {
            Transition(
                recordId,
                AttendanceState.Published,
                actor,
                role,
                "Attendance published");
        }

        // 5️⃣ Request Reopen
        public void RequestReopen(Guid recordId, string actor, RoleType role, string reason)
        {
            Transition(
                recordId,
                AttendanceState.ReopenRequested,
                actor,
                role,
                reason);
        }

        // 6️⃣ Apply Correction (NEW VERSION)
        public void ApplyCorrection(
            Guid recordId,
            Dictionary<string, bool> correctedAttendance,
            string actor,
            RoleType role,
            string reason)
        {
            var record = _repo.GetRecord(recordId);

            if (!AttendanceStateMachine.CanTransition(
                record.CurrentState,
                AttendanceState.Corrected,
                role))
                throw new Exception("Invalid correction request");

            var latestVersion = _repo.GetLatestVersion(record.Id);

            var newVersion = new AttendanceVersion(
                record.Id,
                latestVersion.VersionNumber + 1,
                correctedAttendance,
                actor,
                role,
                reason);

            record.ChangeState(AttendanceState.Corrected);
            record.SetCurrentVersion(newVersion.Id);

            _repo.SaveVersion(newVersion);

            _repo.SaveAudit(new AuditLog(
                record.Id,
                "Apply Correction",
                AttendanceState.ReopenRequested,
                AttendanceState.Corrected,
                actor,
                role,
                reason));
        }

        // 🔒 CENTRALIZED TRANSITION GUARD
        private void Transition(
            Guid recordId,
            AttendanceState targetState,
            string actor,
            RoleType role,
            string reason)
        {
            var record = _repo.GetRecord(recordId);

            if (!AttendanceStateMachine.CanTransition(
                record.CurrentState,
                targetState,
                role))
                throw new Exception(
                    $"Transition {record.CurrentState} → {targetState} not allowed");

            var from = record.CurrentState;
            record.ChangeState(targetState);

            _repo.SaveAudit(new AuditLog(
                record.Id,
                $"Move to {targetState}",
                from,
                targetState,
                actor,
                role,
                reason));
        }
    }
}
