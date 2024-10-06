using Microsoft.AspNetCore.Mvc;
using HockeyClubAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HockeyClubAPI.MvcControllers
{
  public class Playercontroller : Controller
  {
    private readonly HockeyClubContext _context;

    public Playercontroller(HockeyClubContext context)
    {
      _context = context;
    }

    // GET: Player
    public async Task<IActionResult> Index()
    {
      var players = await _context.Players.ToListAsync();
      return View(players);
    }

    // GET: Player/Create
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create([Bind("Fullname,Position,JerseyNumber,DateOfBirth")] Player player)
    {
      if (ModelState.IsValid)
      {
        _context.Add(player);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
    }

  }


}