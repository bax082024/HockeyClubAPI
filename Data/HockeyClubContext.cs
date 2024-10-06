using HockeyClubAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HockeyClubAPI.Data
{
  public class HockeyClubContext : IdentityDbContext<ApplicationUser>
  {
    public HockeyClubContext(DbContextOptions<HockeyClubContext> options)
      : base(options)
      {
        
      }

  }
}