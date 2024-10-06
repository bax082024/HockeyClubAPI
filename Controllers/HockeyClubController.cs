using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class HockeyClubController : ControllerBase
{
  // only Admin has full access
  [Authorize(Roles = "Admin")]
  [HttpGet("Admin")]
  public IActionResult GetAdminData()
  {
    return Ok("Admin: Full access to the system and server");
  }

  // Leader has access to all system imformation except server code.
  [Authorize(Roles = "Leader")]
  [HttpGet("Leader")]

  public IActionResult GetLeaderData()
  {
    return Ok("Leader: Leader has access to all system imformation except server code.")
  }

  // Office has access to accounting and administration documents.
  [Authorize(Roles = "Office")]
  [HttpGet("Office")]

  public IActionResult GetOfficeData()
  {
    return Ok("Office has access to accounting and administration documents.")
  }


}