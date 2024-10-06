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

      public DbSet<Player> Players { get; set; }
      //public DbSet<Schedule> Schedules { get; set; }
      //public DbSet<Announcement> Announcements { get; set; }

  }
}