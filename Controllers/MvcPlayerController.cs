using Microsoft.AspNetCore.Mvc; 
using HockeyClubAPI.Data; 
using Microsoft.EntityFrameworkCore; 
using HockeyClubAPI.Models; 


namespace HockeyClubAPI.MvcControllers
{
    public class PlayerController : Controller
    {
        private readonly HockeyClubContext _context;

        public PlayerController(HockeyClubContext context)
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,Position,JerseyNumber,DateOfBirth")] Player player)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            _context.Add(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
