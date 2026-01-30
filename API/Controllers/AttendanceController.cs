using DISLAMS_Assignment.Application.Services;
using DISLAMS_Assignment.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DISLAMS_Assignment.API.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceService _service;

        public AttendanceController(AttendanceService service)
        {
            _service = service;
        }

        [HttpPost("draft")]
        public IActionResult CreateDraft()
        {
            var id = _service.CreateDraft(
                DateOnly.FromDateTime(DateTime.Today),
                "Class-10-A",
                new Dictionary<string, bool>
                {
                { "S1", true },
                { "S2", false }
                },
                "teacher_1",
                RoleType.Teacher);

            return Ok(id);
        }

        [HttpPost("{id}/submit")]
        public IActionResult Submit(Guid id)
        {
            _service.Submit(id, "teacher_1", RoleType.Teacher);
            return Ok();
        }

        [HttpPost("{id}/approve")]
        public IActionResult Approve(Guid id)
        {
            _service.Approve(id, "admin_1", RoleType.Admin);
            return Ok();
        }

        [HttpPost("{id}/publish")]
        public IActionResult Publish(Guid id)
        {
            _service.Publish(id, "admin_1", RoleType.Admin);
            return Ok();
        }

        [HttpPost("{id}/reopen")]
        public IActionResult RequestReopen(Guid id)
        {
            _service.RequestReopen(
                id,
                "teacher_1",
                RoleType.Teacher,
                "Parent dispute raised");

            return Ok();
        }

        [HttpPost("{id}/correct")]
        public IActionResult Correct(Guid id)
        {
            _service.ApplyCorrection(
                id,
                new Dictionary<string, bool>
                {
                { "S1", true },
                { "S2", true }
                },
                "admin_1",
                RoleType.Admin,
                "Correction after verification");

            return Ok();
        }
    }
}
