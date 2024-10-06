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
  }


}