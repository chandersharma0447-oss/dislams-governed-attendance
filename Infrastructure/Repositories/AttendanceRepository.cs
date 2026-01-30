using System;
using DISLAMS_Assignment.Domain.Entities;
using DISLAMS_Assignment.Infrastructure.Persistence;

namespace DISLAMS_Assignment.Infrastructure.Repositories
{
    public class AttendanceRepository
    {
        private readonly AppDBContext _db;

        public AttendanceRepository(AppDBContext db)
        {
            _db = db;
        }

        public AttendanceRecord GetRecord(Guid id)
        {
            var record = _db.Records.SingleOrDefault(r => r.Id == id);
            if (record == null)
                throw new InvalidOperationException($"AttendanceRecord with Id {id} does not exist.");
            return record;
        }

        public void SaveRecord(AttendanceRecord record)
        {
            _db.Records.Add(record);
            _db.SaveChanges();
        }

        public void SaveVersion(AttendanceVersion version)
        {
            _db.Versions.Add(version);
            _db.SaveChanges();
        }

        public AttendanceVersion GetLatestVersion(Guid recordId)
        {
            var version = _db.Versions.Where(v => v.AttendanceRecordId == recordId)
                .OrderByDescending(v => v.VersionNumber)
                .First();

            if (version == null)
                throw new InvalidOperationException($"VersionRecord with Id {recordId} does not exist.");
            return version;
        }

        public void SaveAudit(AuditLog log)
        {
            _db.Audits.Add(log);
            _db.SaveChanges();
        }
    }
}
