using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HockeyClubAPI.Controllers
{
    [ApiController]
    [Route("api/hockey")]
    public class HockeyClubController : ControllerBase
    {
        // Disable Authorize to check individual localhost

        // Only Admin has full access
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult GetAdminData()
        {
            return Ok("Admin: Full access to the system and server");
        }

        // Leader has access to all system information except server code.
        [Authorize(Roles = "Leader")]
        [HttpGet("Leader")]
        public IActionResult GetLeaderData()
        {
            return Ok("Leader: Leader has access to all system information except server code.");
        }

        // Office has access to accounting and administration documents.
        [Authorize(Roles = "Office")]
        [HttpGet("Office")]
        public IActionResult GetOfficeData()
        {
            return Ok("Office has access to accounting and administration documents.");
        }

        // Trainer has access to trainer files and related documents
        [Authorize(Roles = "Trainer")]
        [HttpGet("Trainer")]
        public IActionResult GetTrainerData()
        {
            return Ok("Trainer has access to trainer files and related documents.");
        }

        // Helper has access to helper-specific documents
        [Authorize(Roles = "Helper")]
        [HttpGet("Helper")]
        public IActionResult GetHelperData()
        {
            return Ok("Helper has access to helper-specific documents.");
        }
    }
}
